using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.DTOModel
{
    public class VehicleDTO
    {
        public string VehicleName { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string VehicleNumber { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
        public string VehicleOwnerId { get; set; } = string.Empty;
        public string? ImageSrc { get; set; }
        public int? VehicleId { get; set; } = 0;
    }
}
