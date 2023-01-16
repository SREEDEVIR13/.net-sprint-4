using Microsoft.AspNetCore.Http;
using RB.Core.Application.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.Interface
{
    public interface IVehicleRegistration
    {
        public UserResponseDTO RegisterVehicle(VehicleDTO vehicleDTO, IFormFile ImageFile);
        public List<VehicleDTO> GetAllVehicle(string VehicleOwnerId, String Scheme, HostString Host, PathString PathBase);
        public UserResponseDTO DeleteVehicle(int vehicleId);
    }
}
