using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Dtos.Auth
{
    public class LoginDto
    {
        public string Token { get; set; }
        public bool IsSupplier { get; set; }
    }
}