//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Mvc;

//namespace Encounter_Me.Controllers
//{
//    [System.Web.Http.Route("api/[controller]")]
//    [ApiController]
//    public class UserDataController : ControllerBase
//    {
//        readonly List<string> userNameList = new();

//        public UserDataController()
//        {
//            userNameList.Add("ankit");
//            userNameList.Add("vaibhav");
//            userNameList.Add("priya");
//        }

//        [System.Web.Http.HttpPost]
//        public IActionResult Post(UserRegistration registrationData)
//        {
//            if (userNameList.Contains(registrationData.Username.ToLower()))
//            {
//                ModelState.AddModelError(nameof(registrationData.Username), "This User Name is not available.");
//                return BadRequest(ModelState);
//            }
//            else
//            {
//                return Ok(ModelState);
//            }
//        }
//    }
//}
