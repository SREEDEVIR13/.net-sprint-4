using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Core.Domain.Models;
using RB.Infrastructure.RB.Infrastructure.Repository;
using RB.Infrastructure.RB.Infrastructure.Services.General.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Infrastructure.RB.Infrastructure.Services.User
{
    public class VehicleRegistration : IVehicleRegistration
    {
        private readonly UserDbContext _userDbContext;
        private readonly ISaveImage _saveImage;
        public VehicleRegistration(UserDbContext userDbContext, ISaveImage saveImage)
        {
            _userDbContext = userDbContext;
            _saveImage = saveImage;
        }

        // delete vehicle by id
        public UserResponseDTO DeleteVehicle(int vehicleId)
        {
            Vehicle vehicle = _userDbContext.Vehicles.FirstOrDefault(i => i.VehicleId == vehicleId);

            var response = new UserResponseDTO();

            if (vehicle != null)
            {
                if (vehicle.Status == 0)
                {
                    vehicle.Status = 1;
                    _userDbContext.Vehicles.Update(vehicle);
                    _userDbContext.SaveChanges();
                    response.Status = true;
                    response.Output = "deleted";
                    return response;
                };

            }
            else
            {
                response.Status = false;
                response.Output = "All ready deleted";
            }
            response.Status = false;
            response.Output = "Id does not exist";
            return response;
        }





        //get vehicle details


        //public List<VehicleDTO> GetAllVehicle(string VehicleOwnerId)


        public List<VehicleDTO> GetAllVehicle(string VehicleOwnerId, string Scheme, HostString Host, PathString PathBase)
        {
            var response = new UserResponseDTO();

            var list = new List<VehicleDTO>();

            var data = _userDbContext.Vehicles.Where(e => e.Status == 0 && e.VehicleOwnerId == VehicleOwnerId).Include(x => x.UserRegister).Select(e => new Vehicle()
            {
                VehicleImage = e.VehicleImage,
                VehicleId = e.VehicleId,
                VehicleName = e.VehicleName,
                VehicleNumber = e.VehicleNumber,
                VehicleOwnerId = e.VehicleOwnerId,
                NumberOfSeats = e.NumberOfSeats,
                VehicleType = e.VehicleType,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Scheme, Host, PathBase, e.VehicleImage)
            }).ToList();

            if (data != null && data.Count > 0)
            {
                foreach (var vehicle in data)
                {


                    VehicleDTO vehicleDTO = new VehicleDTO()
                    {
                        VehicleId = vehicle.VehicleId,
                        VehicleOwnerId = vehicle.VehicleOwnerId,
                        VehicleNumber = vehicle.VehicleNumber,
                        VehicleName = vehicle.VehicleName,
                        VehicleType = vehicle.VehicleType,
                        NumberOfSeats = vehicle.NumberOfSeats,
                        ImageSrc = vehicle.ImageSrc
                    };



                    list.Add(vehicleDTO);
                }
                response.Status = true;
                response.Output = "vehicle listed successfully";
                return list;

            }
            else
            {
                response.Status = false;
                response.Output = "No vehicle list";
                return list;
            }
        }

        public UserResponseDTO RegisterVehicle(VehicleDTO vehicleDTO, IFormFile ImageFile)
        {
            var response = new UserResponseDTO();
            try
            {
                string imagepath = _saveImage.UploadImage(ImageFile, vehicleDTO.VehicleName);
                var user = _userDbContext.Users.Where(x => x.EmployeeId == vehicleDTO.VehicleOwnerId).ToList();
                var exist = _userDbContext.Vehicles.Where(x => x.VehicleNumber == vehicleDTO.VehicleNumber).ToList();

                if (!exist.Any())
                {
                    var vehicle = new Vehicle()
                    {
                        VehicleName = vehicleDTO.VehicleName,
                        VehicleNumber = vehicleDTO.VehicleNumber,
                        VehicleType = vehicleDTO.VehicleType,
                        NumberOfSeats = vehicleDTO.NumberOfSeats,
                        VehicleImage = imagepath,
                        VehicleOwnerId = user[0].EmployeeId
                    };
                    _userDbContext.Vehicles.Add(vehicle);
                    _userDbContext.SaveChanges();
                }
                else
                {
                    response.Status = false;
                    response.Output = "Vehicle already registered";
                    return response;
                }



            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Output = ex.Message;
                return response;
            }
            response.Status = true;
            response.Output = "vehicle added successfully";
            return response;

        }
    }
}
