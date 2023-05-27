using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Dtos.Order
{
    public class GetUserOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<GetProductDto> Products { get; set; }
    }
}