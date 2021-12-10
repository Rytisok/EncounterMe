using Encounter_Me;
using Encounter_Me.Api.Controllers;
using Encounter_Me.Api.Models;
using Encounter_Me.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EncounterMeTests
{
    public class UserDataControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserController _userController;

        public UserDataControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userController = new UserController(_mockUserRepository.Object);
        }


        [Fact]
        public void GetAllUsers_WhenCalled_ReturnsOkResult()
        {
            //Act
            var okResult = _userController.GetAllUsers();

            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAllUsers_WhenCalled_ReturnsExactNumberOfUsers()
        {
            //Arrange
            _mockUserRepository.Setup(repos => repos.GetAllUsers())
                .Returns(new List<UserData>() { new UserData()});

            //Act
            var result = _userController.GetAllUsers();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result as OkObjectResult);
            var users = Assert.IsType<List<UserData>>(okResult.Value);

            Assert.Single(users);
        }

        [Fact]
        public void GetUserById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            //Act
            var notFoundResult = _userController.GetUserById(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetUserById_ExistingGuidPassed_ReturnsOkResult()
        {
            //Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var testUser = new UserData(testGuid, Factions.Red, "Un", "Fn", "Ln", "e@mail", "Psword1");
            _mockUserRepository.Setup(repos => repos.GetUserById(testGuid)).Returns(testUser);

            //Act
            var okResult = _userController.GetUserById(testGuid);

            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetUserById_ExistingGuidPassed_ReturnsRightUser()
        {
            //Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var testUser = new UserData(testGuid, Factions.Red, "Un", "Fn", "Ln", "e@mail", "Psword1");
            _mockUserRepository.Setup(repos => repos.GetUserById(testGuid)).Returns(testUser);

            //Act
            var okResult = _userController.GetUserById(testGuid) as OkObjectResult;

            //Assert
            Assert.IsType<UserData>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as UserData).Id);
        }


        [Fact]
        public void CreateUser_GotNullAsParameter_ReturnsBadRequestResult()
        {
            //Act
            var badRequest = _userController.CreateUser(null);

            //Assert
            Assert.IsType<BadRequestResult>(badRequest);
        }

        [Fact]
        public void CreateUser_GotDataWithTakenUserName_ReturnsBadRequestResult()
        {
            //Arrange
            var testUser = new UserData(Guid.NewGuid(), Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");
            _mockUserRepository.Setup(repos => repos.IsUsernameTaken("Usrname")).Returns(true);

            //Act
            var badRequest = _userController.CreateUser(testUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void CreateUser_GotDataWithTakenEmail_ReturnsBadRequestResult()
        {
            //Arrange
            var testUser = new UserData(Guid.NewGuid(), Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");
            _mockUserRepository.Setup(repos => repos.IsEmailTaken("e@mail")).Returns(true);

            //Act
            var badRequest = _userController.CreateUser(testUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void CreateUser_GotValidData_ExecutedAddUserOnce()
        {
            //Arrange
            UserData user = null;
            _mockUserRepository.Setup(repos => repos.AddUser(It.IsAny<UserData>())).Callback<UserData>(x => user = x);
            var testUser = new UserData(Guid.NewGuid(), Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");

            //Act
            _userController.CreateUser(testUser);
            //_userController.CreateUser(testUser); - fails the test.

            //Assert
            _mockUserRepository.Verify(x => x.AddUser(It.IsAny<UserData>()), Times.Once);
        }

        [Fact]
        public void CreateUser_GotValidData_ReturnsCreated()
        {
            //Arrange
            var testUser = new UserData(Guid.NewGuid(), Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");

            //Act
            var createdResponse = _userController.CreateUser(testUser);
            
            //Assert
            Assert.IsType<CreatedResult>(createdResponse);
        }


        [Fact]
        public void UpdateUser_GotInvalidData_ReturnsBadRequest()
        {
            //Act
            var badRequest = _userController.UpdateUser(null);

            //Assert
            Assert.IsType<BadRequestResult>(badRequest);
        }


        [Fact]
        public void UpdateUser_GotDataWithTakenUserName_ReturnsBadRequestResult()
        {
            //Arrange
            var testUser = new UserData(Guid.NewGuid(), Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");
            _mockUserRepository.Setup(repos => repos.IsUsernameTaken("Usrname")).Returns(true);

            //Act
            var badRequest = _userController.UpdateUser(testUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void UpdateUser_GotDataWithTakenEmail_ReturnsBadRequestResult()
        {
            //Arrange
            var testUser = new UserData(Guid.NewGuid(), Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");
            _mockUserRepository.Setup(repos => repos.IsEmailTaken("e@mail")).Returns(true);

            //Act
            var badRequest = _userController.UpdateUser(testUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void UpdateUser_GotDataWithNonExistingGuid_ReturnsNotFound()
        {
            //Arrange
            var testUser = new UserData(Guid.NewGuid(), Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");
            //Act
            var notFoundResult = _userController.UpdateUser(testUser);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void UpdateUser_GotValidData_ReturnsNoContentResult()
        {
            //Arrange
            var testGuid = Guid.NewGuid();
            var testUser = new UserData(testGuid, Factions.Red, "Usrname", "Fn", "Ln", "e@mail", "Psword1");
            _mockUserRepository.Setup(repos => repos.GetUserById(testGuid)).Returns(testUser);
            _mockUserRepository.Setup(repos => repos.UpdateUser(testUser)).Returns(testUser);

            //Act
            var noContentResult = _userController.UpdateUser(testUser);
            Assert.IsType<NoContentResult>(noContentResult);
        }


        [Fact]
        public void DeleteUser_GotEmptyGuid_ReturnsBadRequest()
        {
            //Act
            var badRequest = _userController.DeleteUser(Guid.Empty);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void DeleteUser_GotUnknownGuid_ReturnsNotFoundResult()
        {
            //Arrange
            var testGuid = Guid.NewGuid();

            //Act
            var notFoundResult = _userController.DeleteUser(testGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }


        [Fact]
        public void DeleteUser_GotValidGuid_ReturnsNoContentResult()
        {
            //Arrange
            var testGuid = Guid.NewGuid();
            _mockUserRepository.Setup(repos => repos.GetUserById(testGuid)).Returns(new UserData());

            //Act
            var noContentResult = _userController.DeleteUser(testGuid);

            //Assert
            Assert.IsType<NoContentResult>(noContentResult);
        }
    }
}
