using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Domain.Models
{
 public  class RequestHandler
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("HostedRides")]
        public int HostedRideId { get; set; }
        public HostedRides HostedRides { get; set; }

        [ForeignKey("UserRegister")]
        public string JoineeId { get; set; }
        public UserRegister UserRegister { get; set; }
        public int Status { get; set; }
    }
}
