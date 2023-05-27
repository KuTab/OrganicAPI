using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool isSupplier { get; set; } = false;
        public double Rating { get; set; }
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; }
        public string Surname { get; set; } = "";
    }
}