using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
    public class HostRideService : IHostRide
    {
        private readonly UserDbContext _userDbContext;
        public HostRideService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }




        //get unique ride detail 

        public List<HostedRidesRequest> GetDetails(int RideId)
        {

            var list = new List<HostedRidesRequest>();
            var data = _userDbContext.HostedRides.FirstOrDefault(i => i.Id == RideId);

            var response = new UserResponseDTO();
            if (data != null)
            {
                HostedRidesRequest hostedRidesRequest = new HostedRidesRequest();
                {
                    hostedRidesRequest.Id = data.Id;
                    hostedRidesRequest.NumberOfSeats= data.NumberOfSeats;
                    hostedRidesRequest.EndLocation=data.EndLocation;
                    hostedRidesRequest.StartLocation=data.StartLocation;

                    

                };

                list.Add(hostedRidesRequest);
                response.Status = true;
                response.Output = "get";
                return list;
            }
            else
            {
                response.Status = false;
                response.Output = "No Rides ";
                return list;
            }
        }


        //  invited status list
        public List<InvitedMembersResponse> GetInvitedMembers(int RideId, string Scheme, HostString Host, PathString PathBase)
        {
            var response = new List<InvitedMembersResponse>();
            var data = _userDbContext.Requests.Where(i => i.HostedRideId == RideId).Include(x => x.UserRegister).ToList();
            if (data != null)
            {
                foreach (var item in data)
                {
                    InvitedMembersResponse member = new InvitedMembersResponse();
                    member.FullName = item.UserRegister.FirstName + " " + item.UserRegister.LastName;
                    member.Status = item.Status;
                    member.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}"
                            , Scheme, Host, PathBase, item.UserRegister.ProfileImageName);
                    response.Add(member);
                    member.InvitationId = item.Id;
                }
                return response;
            }
            else
            {
                return response;
            }
        }






        //get hosted rides list
        public List<HostedRidesRequest> GetRides(string VehicleOwnerId)
        {
            var list = new List<HostedRidesRequest>();
            var rides = _userDbContext.HostedRides.Where(i => i.HostId == VehicleOwnerId).ToList();
            var response = new UserResponseDTO();
            if (rides != null && rides.Count > 0)
            {
                foreach (var hostedRides in rides)

                {


                    HostedRidesRequest hostedRidesRequest = new HostedRidesRequest()
                    {
                        Id=hostedRides.Id,
                        StartDate=hostedRides.StartDate,
                        StartTime=hostedRides.StartTime,
                        StartLocation=hostedRides.StartLocation,
                        EndLocation=hostedRides.EndLocation,
                        NumberOfSeats=hostedRides.NumberOfSeats,

                    };



                    list.Add(hostedRidesRequest);
                }
                response.Status = true;
                response.Output = "Rides listed successfully";
                return list;

            }
            else
            {
                response.Status = false;
                response.Output = "NO hosted Rides";
                return list;
            }

        }















        public UserResponseDTO HostRide(HostedRidesRequest hostedRides)
        {
            

            UserResponseDTO response = new UserResponseDTO();

            var user = _userDbContext.Users.Where(x => x.EmployeeId == hostedRides.MemberId).ToList();
            var vehicles = _userDbContext.Vehicles.Where(x => x.VehicleId == hostedRides.VehicleId).ToList();
            if (user.Count != 0)
            {
              
                if (vehicles.Count != 0)
                {
                    var hosted = new HostedRides()
                    {
                        StartLocation = hostedRides.StartLocation,
                        EndLocation = hostedRides.EndLocation,
                        StartDate = hostedRides.StartDate.ToString(),
                        StartTime = hostedRides.StartTime.ToString(),
                        HostId = user[0].EmployeeId,
                        VehicleId = vehicles[0].VehicleId,
                        NumberOfSeats = vehicles[0].NumberOfSeats,
                    };
                    _userDbContext.Add(hosted);
                    _userDbContext.SaveChangesAsync();

                    response.Output = "Ride hosted succesfully";
                    response.Status = true;
                    return response;
                }
                else
                {
                    response.Output = "Ride host unsuccesfull - Vehicle not found ";
                    response.Status = false;
                    return response;

                }

            }
            else
            {
                response.Output = "Ride host unsuccesfull - Member id is not fetched";
                response.Status = false;
                return response;

            }
        }
    }
}
