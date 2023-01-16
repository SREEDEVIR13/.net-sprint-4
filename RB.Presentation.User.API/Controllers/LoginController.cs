using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;

namespace RB.Presentation.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserLogin _userLogin;
        public LoginController(IUserLogin userLogin)
        {
            _userLogin = userLogin;
        }

        [HttpPost]
        public IActionResult UserLogin([FromForm] UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {
                return BadRequest();
            }
            var response = _userLogin.Login(userLoginDTO);
            return Ok(response);
        }
    }
}
