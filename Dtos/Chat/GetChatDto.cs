using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Dtos.Chat
{
    public class GetChatDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public string ReceiverName { get; set; }
    }
}