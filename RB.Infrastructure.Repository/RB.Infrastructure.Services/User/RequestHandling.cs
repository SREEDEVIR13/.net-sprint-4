using MediatR;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Core.Domain.Models;
using RB.Infrastructure.RB.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Infrastructure.RB.Infrastructure.Services.User
{
    public class RequestHandling : IRequestHandling
    {
        private readonly UserDbContext _userDbContext;

        public RequestHandling(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public UserResponseDTO RequestHandle(RequestHandlerDTO request)
        {
            var response = new UserResponseDTO();
            try
            {

                for (int k = 0; k < request.JoineeId.Count; k++)
                {
                    var userId = _userDbContext.Users.Where(i => i.EmployeeId == request.JoineeId[k]).FirstOrDefault();
                    var rideRequest = new RequestHandler()
                    {
                        HostedRideId = request.HostedRideId,
                        JoineeId = userId.EmployeeId,
                        Status = request.Status,
                    };
                    _userDbContext.Requests.Add(rideRequest);
                    _userDbContext.SaveChanges();
                }

                response.Status = true;
                response.Output = "request send";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Output = ex.Message;
                return response;

            }
        }
    }
}


