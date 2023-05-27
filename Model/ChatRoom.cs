using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Model
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public User User { get; set; }
        public User Supplier { get; set; }
    }
}