using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.DTOModel
{
    public class UserRegisterDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public double Number { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        
       // public IFormFile? LicenseFile { get; set; }
        public string Role { get; set; } = string.Empty;
        public string ProfileSrc { get; set; } = string.Empty;
    }
}
