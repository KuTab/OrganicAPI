using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Model
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
    }
}