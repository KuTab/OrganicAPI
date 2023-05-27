using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Model
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}