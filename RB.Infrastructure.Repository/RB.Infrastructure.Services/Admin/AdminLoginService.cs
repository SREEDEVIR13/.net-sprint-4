using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Infrastructure.RB.Infrastructure.Repository;
using RB.Infrastructure.RB.Infrastructure.Services.General.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Infrastructure.RB.Infrastructure.Services.Admin
{
    public class AdminLoginService : IAdminLogin
    {
        private readonly UserDbContext _userDbContext;
        private readonly IRegisterValidations _registerValidations;
        public AdminLoginService(UserDbContext userDbContext, IRegisterValidations registerValidations)
        {
            _userDbContext = userDbContext;
            _registerValidations = registerValidations;
        }

        public UserResponseDTO Login(AdminLoginRequest login)
        {
            var response = new UserResponseDTO();
            try
            {
               
                var user = _userDbContext.Users.FirstOrDefault(u => u.Email == login.username);

                if (user == null)
                {
                    response.Output = "user not found";
                    response.Status = false;
                    return response;
                }
                if (!_registerValidations.VerifyPasswordHash(login.password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Output = "wrong password";
                    response.Status = false;
                    return response;

                }
                var user1 = new UserRegisterDTO()
                {
                    Email = login.username,
                };

                string token = _registerValidations.CreateToken(user1);
                response.Output = token;
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Output = ex.Message;
                return response;
            }

        }
    }
}
