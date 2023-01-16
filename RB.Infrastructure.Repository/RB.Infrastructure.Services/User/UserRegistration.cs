using Microsoft.AspNetCore.Http;
using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Core.Domain.Models;
using RB.Infrastructure.RB.Infrastructure.Repository;
using RB.Infrastructure.RB.Infrastructure.Services.General.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Infrastructure.RB.Infrastructure.Services.User
{
    public class UserRegistration : IUserRegistration
    {
        private readonly UserDbContext _userDbContext;
        private readonly IRegisterValidations _registerValidations;
        private readonly ISaveImage _saveImage;
        public UserRegistration(UserDbContext userDbContext, IRegisterValidations registerValidations, ISaveImage saveImage)
        {
            _userDbContext = userDbContext;
            _registerValidations = registerValidations;
            _saveImage = saveImage;
        }
        public UserResponseDTO Register(UserRegisterDTO userRegisterDTO , IFormFile LisenceFile, IFormFile ProfileFile)
        {
            var response = new UserResponseDTO(); 
            try 
            {
                _registerValidations.CreatePasswordHash(userRegisterDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var lisence = _saveImage.UploadImage(LisenceFile, userRegisterDTO.FirstName+userRegisterDTO.LastName);
                var profile = _saveImage.UploadImage(ProfileFile, userRegisterDTO.FirstName + userRegisterDTO.LastName);
               
                var user = new TempUserRegister
                {
                    FirstName = userRegisterDTO.FirstName,
                    LastName = userRegisterDTO.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = userRegisterDTO.Email,
                    Gender = userRegisterDTO.Gender,
                    Number = userRegisterDTO.Number,
                    Department = userRegisterDTO.Department,
                    EmployeeId = userRegisterDTO.EmployeeId,
                    Role = userRegisterDTO.Role,
                    LicenceImageName = lisence,
                    ProfileImageName = profile,
                };
                _userDbContext.TempUsers.Add(user);
                _userDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Output = ex.Message;
                return response;
            }
            response.Status = true;
            response.Output = "user waiting for confirmation";
            return response;



        }

        public UserResponseDTO ConfirmUser(string email)
        {
            var response = new UserResponseDTO();
            try
            {

                var data = _userDbContext.TempUsers.Where(x => x.Email == email).ToList();
                {
                    UserRegister user = new UserRegister()
                    {
                        Email = email,
                        FirstName = data[0].FirstName,
                        LastName = data[0].LastName,
                        PasswordHash = data[0].PasswordHash,
                        PasswordSalt = data[0].PasswordSalt,
                        EmployeeId = data[0].EmployeeId,
                        Gender = data[0].Gender,
                        Number = data[0].Number,
                        Department = data[0].Department,
                        LicenceImageName = data[0].LicenceImageName,
                        ProfileImageName = data[0].ProfileImageName,
                        Role = data[0].Role,
                    };
                    _userDbContext.Users.Add(user);

                    _userDbContext.SaveChanges();
                }
               var temp = _userDbContext.TempUsers.Where(x => x.Email == email).ToList();
                _userDbContext.TempUsers.Remove(temp[0]);
                _userDbContext.SaveChanges();


            }
            catch (Exception ex)
            {

                response.Status = false;
                response.Output = ex.Message;
                return response;
            }
            response.Status = true;
            response.Output = "user added succefully";
            return response;
        }
        public List<UserRegisterDTO> GetUserDetail(string VehicleOwnerId)
        {
            var list = new List<UserRegisterDTO>();
            var number = _userDbContext.Users.FirstOrDefault(i => i.EmployeeId == VehicleOwnerId);

            var response = new UserResponseDTO();
            if (number != null)
            {
                UserRegisterDTO userRegisterDTO = new UserRegisterDTO();
                {
                    userRegisterDTO.EmployeeId = number.EmployeeId;
                    userRegisterDTO.Number = number.Number;
                    userRegisterDTO.Department = number.Department;
                    userRegisterDTO.Role = number.Role;
                    userRegisterDTO.Gender = number.Gender;
                    userRegisterDTO.LastName = number.LastName;
                    userRegisterDTO.FirstName = number.FirstName;


                };

                list.Add(userRegisterDTO);
                response.Status = true;
                response.Output = "get";
                return list;
            }
            else
            {
                response.Status = false;
                response.Output = "No Number ";
                return list;
            }

        }
    }
}
