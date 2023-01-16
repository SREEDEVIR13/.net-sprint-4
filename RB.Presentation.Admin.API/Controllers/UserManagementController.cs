using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB.Core.Application.Interface;
using RB.Infrastructure.RB.Infrastructure.Services.User;

namespace RB.Presentation.Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IAdminUserManagement _adminUserManagement;
        public UserManagementController(IAdminUserManagement adminUserManagement)
        {
            _adminUserManagement = adminUserManagement;
        }
        [HttpGet]
        [Route("getallusers")]
        public IActionResult GetAllUsers()
        {
            
            var response =_adminUserManagement.GetAllUsers(Request.Scheme, Request.Host, Request.PathBase);
            return Ok(response);

        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid request");

            }
            var response = _adminUserManagement.DeactivateUser(id);
            return Ok(response);
        }
    }
}
