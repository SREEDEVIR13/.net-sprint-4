using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB.Core.Application.Interface;

namespace RB.Presentation.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        private readonly ICallOption _callOption;
        //private  UserRegisterDTO userRegisterDTO;

        public CallController(ICallOption callOption)

        {
            _callOption = callOption;

        }

        [HttpGet]
        [Route("getNumber/{OwnerId}")]
        public IActionResult GetPhoneNumber(string OwnerId)




        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid request");
            }
            var response = _callOption.GetNumber(OwnerId);
            return Ok(response);




        }
    }
}
