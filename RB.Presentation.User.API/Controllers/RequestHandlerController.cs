using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;

namespace RB.Presentation.User.API.Controllers
{
    
    
        [Route("api/[controller]")]
        [ApiController]
        public class RequestHandlerController : ControllerBase
        {
            private readonly IRequestHandling _request;
            public RequestHandlerController(IRequestHandling request)
            {
                _request = request;
            }
            [HttpPost]
            public IActionResult HandleRequest(RequestHandlerDTO requestHandler)
            {
                var response = _request.RequestHandle(requestHandler);
                return Ok(response);
            }
        }
}
