using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;

namespace RB.Presentation.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRegistration _userRegistration;
        public RegistrationController(IUserRegistration userRegistration)
        {
            _userRegistration = userRegistration;
        }
        [HttpGet]
        [Route("getDetail/{OwnerId}")]
        public IActionResult GetDetail(string OwnerId)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid request");
            }
            var response = _userRegistration.GetUserDetail(OwnerId);
            return Ok(response);
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromForm] UserRegisterDTO userRegisterDTO , IFormFile LisenceFile, IFormFile ProfileFile)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("not a valid request");
            }
            var response= _userRegistration.Register(userRegisterDTO , LisenceFile, ProfileFile);

            return Ok(response);
           
        }
        [HttpPost]
        [Route("Confirm-user")]
        public IActionResult ConfirmMail(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid request");
            }
            var response = _userRegistration.ConfirmUser(email);
            return Ok(response);
        }
    }
}
