using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetUserDto>> GetMe(int userId)
        {
            var response = new ServiceResponse<GetUserDto>();
            var user = await _context.Users.FirstAsync(x => x.Id == userId);
            response.Data = _mapper.Map<GetUserDto>(user);
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateUser(UpdateUserDto newUser)
        {
            var response = new ServiceResponse<string>();
            if (! (new EmailAddressAttribute().IsValid(newUser.Email))) {
                response.Success = false;
                response.Message = "Неверный email";
                return response;
            }
            var user = _context.Users.First(x => x.Id == newUser.Id);
            var anotherUser = _context.Users.First(x => x.Email == newUser.Email);
            if(user != null)
            {
                if(anotherUser != null && anotherUser.Id != newUser.Id) {
                    response.Success = false;
                    response.Message = "Пользователь с таким email уже есть";
                    return response;
                }

                user.Email = newUser.Email;
                user.Address = newUser.Address;
                user.Phone = newUser.Phone;
                user.Name = newUser.Name;
                user.Surname = newUser.Surname;
                user.Description = newUser.Description;
                _context.SaveChangesAsync();
                response.Success = true;
                response.Data = "Информация успешно обновлена";
                return response;
            }
            response.Success = false;
            response.Message = "Нет пользователя с таким ID";
            return response;
        }
    }
}