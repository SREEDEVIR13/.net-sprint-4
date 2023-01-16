using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RB.Core.Domain.Models
{
    public class UserRegister
    {
        [Key]
        public string EmployeeId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public double Number { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? LicenceImageName { get; set; }
        public string ProfileImageName { get; set; }= string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool Status { get; set; } = false; //to set for soft delete of user

        //public ICollection<HostedRides> HostedRides { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        //public ICollection<MembersJoined> MembersJoined { get; set; }
    }
}
