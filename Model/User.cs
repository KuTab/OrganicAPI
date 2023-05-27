using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool isSupplier { get; set; } = false;
        public double Rating { get; set; } = 0;
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Surname { get; set; } = "";
    }
}