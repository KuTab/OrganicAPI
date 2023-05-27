using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<LoginDto>> Login(string email, string password);

        Task<bool> UesrExists(string email);
    }
}