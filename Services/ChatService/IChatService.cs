using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.ChatService
{
    public interface IChatService
    {
        public Task<ServiceResponse<int>> CreateChat(int userId, int supplierId);
        public Task<ServiceResponse<int>> GetChat(int userId, int supplierId);
        public Task<ServiceResponse<List<GetChatDto>>> GetMyChats(int userId, bool isSupplier);
    }
}