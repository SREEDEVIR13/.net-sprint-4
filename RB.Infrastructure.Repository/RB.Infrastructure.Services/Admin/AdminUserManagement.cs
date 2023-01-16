using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Infrastructure.RB.Infrastructure.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RB.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace RB.Infrastructure.RB.Infrastructure.Services.Admin
{
    public class AdminUserManagement : IAdminUserManagement
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public AdminUserManagement(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public List<UserRegisterDTO> GetAllUsers(String Scheme, HostString Host, PathString PathBase)
        {
            

            var list = new List<UserRegisterDTO>();
            var data = _userDbContext.Users.Where(i => i.Status == false).Select(e => new UserRegister()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Department = e.Department,
                Email = e.Email,
                EmployeeId = e.EmployeeId,
                Gender = e.Gender,
                LicenceImageName = e.LicenceImageName,
                Number = e.Number,
                ProfileImageName = e.ProfileImageName,


            }).ToList();

            if(data!= null && data.Count > 0)
            {
                foreach(var item in data)
                {
                    UserRegisterDTO user = new UserRegisterDTO()
                    {
                        FirstName =item.FirstName,
                        LastName =item.LastName,
                        Email =item.Email,
                        EmployeeId =item.EmployeeId,
                        Department =item.Department,
                        Gender =item.Gender,
                        Number =item.Number,
                        ProfileSrc = String.Format("{0}://{1}{2}/Images/{3}", Scheme, Host, PathBase, item.ProfileImageName)
                    };
                    list.Add(user);
                }
                return list;
            }
            else
            {
                return list;
            }
                        
        }

        
        public UserResponseDTO DeactivateUser(string id)
        {
            UserRegister user = _userDbContext.Users.FirstOrDefault(i => i.EmployeeId == id);
            var response = new UserResponseDTO();
            if(user != null)
            {
                if(user.Status == false)
                {
                    user.Status = true;
                    _userDbContext.Users.Update(user);
                    _userDbContext.SaveChanges();
                    response.Status = true;
                    response.Output = "user is deactivated";
                    return response;
                }
                else
                {
                    response.Status = false;
                    response.Output = "user already deactivated";
                    return response;
                }
                
            }
            else
            {
                response.Status = false;
                response.Output = "user does not exist";
                return response;
            }
        }
    }
}
