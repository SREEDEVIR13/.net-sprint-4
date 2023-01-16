using RB.Core.Application.DTOModel;
using RB.Core.Application.Interface;
using RB.Infrastructure.RB.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Infrastructure.RB.Infrastructure.Services.User
{
    public class CallOption : ICallOption
    {

        private readonly UserDbContext _userDbContext;

        public CallOption(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

        }


        public List<UserRegisterDTO> GetNumber(string VehicleOwnerId)

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
