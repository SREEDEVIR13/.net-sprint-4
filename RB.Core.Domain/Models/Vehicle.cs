using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Domain.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; } = 0;
        public string VehicleName { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string VehicleNumber { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
        public int Status { get; set; }
        public string VehicleImage { get; set; } = string.Empty;

        [NotMapped]
        public string ImageSrc { get; set; }


        [ForeignKey("UserRegister")]
        public string VehicleOwnerId { get; set; }
        public UserRegister UserRegister { get; set; }



    }
}
