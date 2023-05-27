using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.UserService
{
    public interface IUserService
    {
        public Task<ServiceResponse<String>> UpdateUser(UpdateUserDto newUser);
        public Task<ServiceResponse<GetUserDto>> GetMe(int userId);
    }
}