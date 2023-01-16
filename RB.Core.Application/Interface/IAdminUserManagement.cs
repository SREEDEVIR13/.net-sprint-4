using Microsoft.AspNetCore.Http;
using RB.Core.Application.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.Interface
{
    public interface IAdminUserManagement
    {
        public List<UserRegisterDTO> GetAllUsers( String Scheme, HostString Host, PathString PathBase);
        public UserResponseDTO DeactivateUser(string id);
    }
}
