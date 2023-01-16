using Microsoft.AspNetCore.Http;
using RB.Core.Application.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.Interface
{
    public interface IUserRegistration
    {
        public UserResponseDTO Register(UserRegisterDTO userRegisterDTO , IFormFile LisenceFile , IFormFile ProfileFile );
        public UserResponseDTO ConfirmUser(string email);

        public List<UserRegisterDTO> GetUserDetail(string VehicleOwnerId);
    }
}
