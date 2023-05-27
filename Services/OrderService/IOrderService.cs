using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.OrderService
{
    public interface IOrderService
    {
        public Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders(int id);

        public Task<ServiceResponse<String>> CreateOreder(CreateOrderDto orderDto, int userId);

        public Task<ServiceResponse<List<GetProductDto>>> GetOrder(int orderId);

        public Task<ServiceResponse<List<GetUserOrderDto>>> GetMyUserOrders(int userId);

        public Task<ServiceResponse<List<GetUserOrderDto>>> GetMySupplierOrders(int userId);
    }
}