using RB.Core.Application.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.Interface
{
    public interface IHostRide
    {
        UserResponseDTO HostRide(HostedRidesRequest hostedRides);
        public List<HostedRidesRequest> GetRides(string VehicleOwnerId);
        public List<HostedRidesRequest> GetDetails(int RideId);
    }
}
