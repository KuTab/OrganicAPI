using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.ChatService
{
    public class ChatService: IChatService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public ChatService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<int>> CreateChat(int userId, int supplierId)
        {
            var chatRoom = new ChatRoom();
            chatRoom.User = await _context.Users.FirstAsync(x => x.Id == userId);
            chatRoom.Supplier = await _context.Users.FirstAsync(x => x.Id == supplierId);
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();
            var response = new ServiceResponse<int>();
            response.Success = true;
            response.Data = chatRoom.Id;
            return response;
        }

        public async Task<ServiceResponse<int>> GetChat(int userId, int supplierId)
        {
            var chatRoom = await _context.ChatRooms.FirstOrDefaultAsync(x => x.User.Id == userId && x.Supplier.Id == supplierId);
            var response = new ServiceResponse<int>();
            if (chatRoom == null) {
                response.Data = (await CreateChat(userId, supplierId)).Data;
                response.Success = true;
                return response;
            }

            response.Data = chatRoom.Id;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<GetChatDto>>> GetMyChats(int userId, bool isSupplier)
        {
            List<ChatRoom> chatRooms;
            if (isSupplier) {
                chatRooms = await _context.ChatRooms.Include(x => x.User).Include(x => x.Supplier).Where(x => x.Supplier.Id == userId).ToListAsync();
            } else {
                chatRooms = await _context.ChatRooms.Include(x => x.User).Include(x => x.Supplier).Where(x => x.User.Id == userId).ToListAsync();
            }
            var response = new ServiceResponse<List<GetChatDto>>();
            var chatList = new List<GetChatDto>();
            if (chatRooms == null) {
                response.Data = chatList;
                response.Success = true;
                return response;
            }

            foreach(var chatRoom in chatRooms) {
                var getChatDto = new GetChatDto();
                getChatDto.Id = chatRoom.Id;
                getChatDto.UserId = chatRoom.User.Id;
                getChatDto.SupplierId = chatRoom.Supplier.Id;
                if (isSupplier) {
                    getChatDto.ReceiverName = chatRoom.User.Name + " " + chatRoom.User.Surname;
                } else {
                    getChatDto.ReceiverName = chatRoom.Supplier.Name + " " + chatRoom.Supplier.Surname;
                }
                chatList.Add(getChatDto);
            }

            response.Data = chatList;
                response.Success = true;
                return response;
        }
    }
}