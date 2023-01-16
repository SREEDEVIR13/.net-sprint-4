using Microsoft.AspNetCore.Mvc;
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
    public class ForgotPassword : IForgotPassword
    {
        private readonly UserDbContext _userDbContext;
        private readonly IRegisterValidations _registerValidations;

        public ForgotPassword(UserDbContext userDbContext, IRegisterValidations registerValidations)
        {
            _userDbContext = userDbContext;
            _registerValidations = registerValidations;
        }

        public string forgotpassword([FromForm] UserRegisterDTO user)
        {
            var token = _registerValidations.CreateToken(user);
            var url = "localhost:3000/change-password?id=" + token;
            return url;
        }

        public UserResponseDTO changepassword([FromForm] string Email, string Password)
        {
            try
            {

                var user = _userDbContext.Users.FirstOrDefault(i => i.Email == Email);
                _registerValidations.CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _userDbContext.Users.Update(user);
                _userDbContext.SaveChanges();

                var response = new UserResponseDTO();
                response.Status = true;
                response.Output = "password changed";
                return response;
            }
            catch (Exception ex)
            {
                var response = new UserResponseDTO();
                response.Status = false;
                response.Output = ex.Message;
                return response;
            }


        }
    }
}
