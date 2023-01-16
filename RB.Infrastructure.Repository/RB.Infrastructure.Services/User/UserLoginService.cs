using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Infrastructure.RB.Infrastructure.Repository;
using RB.Infrastructure.RB.Infrastructure.Services.General.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Infrastructure.RB.Infrastructure.Services.User
{
    public class UserLoginService : IUserLogin
    {
        private readonly UserDbContext _dbContext;
        private readonly IRegisterValidations _registerValidations;
        public UserLoginService(UserDbContext dbContext, IRegisterValidations registerValidations)
        {
            _dbContext = dbContext;
            _registerValidations = registerValidations;
        }
        public UserResponseDTO Login(UserLoginDTO userLoginDTO)
        {
            var response = new UserResponseDTO();
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == userLoginDTO.Username);
                if (user == null)
                {
                    response.Output = "user not found";
                    response.Status = false;
                    return response;
                }
                if (!_registerValidations.VerifyPasswordHash(userLoginDTO.Password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Output = "wrong password";
                    response.Status = false;
                    return response;
                }
                var user1 = new UserRegisterDTO()
                {
                    Email = userLoginDTO.Username,
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
