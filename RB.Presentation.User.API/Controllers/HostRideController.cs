using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Infrastructure.RB.Infrastructure.Services.User;

namespace RB.Presentation.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostRideController : ControllerBase
    {
        private readonly IHostRide _hostRide;
        public HostRideController(IHostRide hostRide)
        {
            _hostRide = hostRide;
        }
        [HttpPost]
        [Route("hostyourride")]
        public IActionResult HostRide(HostedRidesRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = _hostRide.HostRide(request);
            if(response.Status==false)
            {
                return BadRequest(response.Output);
            }
            return Ok(response.Output);

        }



        [HttpGet]
        [Route("getRides/{OwnerId}")]
        public IActionResult GetHostedRides(string OwnerId)




        {
           
            var response = _hostRide.GetRides(OwnerId);
            return Ok(response);




        }




        [HttpGet]
        [Route("getDetailRide")]
        public IActionResult GetRideDetail(int Id)

        {
            
            var response = _hostRide.GetDetails( Id);
            return Ok(response);

        }
        [HttpGet]
        [Route("getInvitedUsers")]
        public IActionResult GetInvitedUsers(int Id)
        {
            var response = _hostRide.GetInvitedMembers(Id, Request.Scheme, Request.Host, Request.PathBase);
            return Ok(response);
        }

    }
}
