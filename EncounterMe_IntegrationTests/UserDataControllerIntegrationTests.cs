using Encounter_Me;
using Encounter_Me.Api;
using Encounter_Me.Shared;
using EncounterMe_IntegrationTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace EncounterMe_IntegrationTests
{
    public class UserDataControllerIntegrationTests : IClassFixture<TestingWebApplicationFactory<Startup>>, IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _httpClient;
        readonly DatabaseFixture Fixture;

        public UserDataControllerIntegrationTests(TestingWebApplicationFactory<Startup> factory, DatabaseFixture fixture)
        {
            _httpClient = factory.CreateClient();
            Fixture = fixture;
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            //Act
            var response = await _httpClient.GetAsync("api/User");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("userName@mail", responseString);
            Assert.Contains("Rr@mail", responseString);
            Assert.Contains("Dd@mail", responseString);
        }

        [Fact]
        public async Task GetUserById_WhenCalled_ReturnsRightUser()
        {
            //Act
            var response = await _httpClient.GetAsync($"api/user/d68ba044-95a1-442b-8754-e329410c766b");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("userName@mail", responseString);
        }

        [Fact]
        public async Task CreateUser_GotWrongData_ReturnsBadRequest()
        {
            //Arrange
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/user");
            postRequest.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", "Username"),
                new KeyValuePair<string, string>("Email", "e@mail"),
                new KeyValuePair<string, string>("Password", "Password1"),
            });

            //Act
            var response = await _httpClient.SendAsync(postRequest);

            //Asssert
            Assert.False(response.IsSuccessStatusCode);
        }


        [Fact]
        public async Task CreateUser_GotValidData_AddsUserToDatabase()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var context = Fixture.CreateContext(transaction);

            //Arrange
            TestDbDataManager.ReinitializeEmptyDbForTests(context);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/user");
            var data = new UserData(Guid.NewGuid(), (Factions)1, "newUser", "Fn", "Ln", "New@mail", "Password1");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Contains("user", responseString);
            Assert.Contains("New@mail", responseString);
            Assert.Equal(1, context.Users.Count<UserData>());
        }

        [Fact]
        public async Task UpdateUser_GotValidData_UpdatesRightUserData()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using var context = Fixture.CreateContext(transaction);

                //Arrange
                var putRequest = new HttpRequestMessage(HttpMethod.Put, "api/user");
                var oldUser = context.Users.First<UserData>();
                var testUser = new UserData(Guid.Parse("d68ba044-95a1-442b-8754-e329410c766b"), (Factions)2, "ChangedUserName", "Fn", "Ln", "userName@mail", oldUser.Password, oldUser.StoredSalt);
                putRequest.Content = new StringContent(JsonSerializer.Serialize(testUser), Encoding.UTF8, "application/json");


                //Act
                var response = await _httpClient.SendAsync(putRequest);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                //Assert
                Assert.True(context.Users.Any(u => u.UserName == "ChangedUserName"));
            }
        }

        [Fact]
        public async Task UpdateUser_GotExistingUsername_ReturnsBadRequest()
        {
            // Assert.True(context.Users.Any(u => u.UserName == "ChangedUserName"));
            using var transaction = Fixture.Connection.BeginTransaction();
            using var context = Fixture.CreateContext(transaction);
            //Arrange
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/user");
            var sameUser = context.Users.First<UserData>();
            postRequest.Content = new StringContent(JsonSerializer.Serialize(sameUser), Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task DeleteUser_GotValidGuid_DeletesUserData()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var context = Fixture.CreateContext(transaction);

            //Arrange
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, "api/user/d68ba044-95a1-442b-8754-e329410c766b");
            var deletedUserId = Guid.Parse("d68ba044-95a1-442b-8754-e329410c766b");

            //Act
            var response = await _httpClient.SendAsync(deleteRequest);
            response.EnsureSuccessStatusCode();

            //Assert
            Assert.False(context.Users.Any(x => x.Id.Equals(deletedUserId)));
        }

        ///TODO :
        /// Written test (it would be theory) to check if all get methods return Ok_status response and correct content type back...
        /// Problem - trailContainer was change to capturePoint, I need to get newest data from main.
    }


}
