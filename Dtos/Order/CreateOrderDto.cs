using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Dtos.Order
{
    public class CreateOrderDto
    {
        public List<ProductOrderDto> Products { get; set; }
    }
}