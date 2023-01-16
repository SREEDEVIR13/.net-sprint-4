using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;

namespace RB.Presentation.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IForgotPassword _forgotPassword;

        public ForgotPasswordController(IForgotPassword forgotPassword)
        {
            _forgotPassword = forgotPassword;
        }
        [HttpPost]
        [Route("forgotpassword")]
        public IActionResult ForgotPassword([FromForm] TempUserDTO data)
        {
            var emailbody = new UserRegisterDTO
            {
                Email = data.Email,
                FirstName = data.Name

            };
            var url = _forgotPassword.forgotpassword(emailbody);


            return Ok(url);

        }

        [HttpPost]
        [Route("changepassword")]
        public IActionResult ChangePassword(string Token, string Password)
        {
            _forgotPassword.changepassword(Token, Password);
            return Ok();
        }
    }
}
