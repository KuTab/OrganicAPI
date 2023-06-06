global using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

         public OrderService(IMapper mapper, DataContext context)
         {
            _context = context;
            _mapper = mapper;
         }  

        public async Task<ServiceResponse<string>> CreateOreder(CreateOrderDto orderDto, int userId) 
        {
            var order = new Order();
            var response = new ServiceResponse<string>();

            var summedProducts = orderDto.Products.GroupBy(x => x.ProductId).ToDictionary(x => x.Count(), x => x.ToList());
            foreach(var product in summedProducts) {
                System.Console.WriteLine($"Count {product.Key}");
                var productInOrder = await _context.Products.FirstAsync(x => x.Id == product.Value[0].ProductId);
                if (product.Key > productInOrder.Quantity) {
                    response.Success = false;
                    response.Message = $"В наличии нет выбранного количества продукта {productInOrder.Title}. Доступно для заказа {productInOrder.Quantity}";
                    return response;
                }
            }

            order.User = await _context.Users.FirstAsync(x => x.Id == userId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach(var prod in orderDto.Products) {
                var product = await _context.Products.FirstAsync(x => x.Id == prod.ProductId);
                product.Quantity -= 1;
                var orderProduct = new OrderProduct();
                orderProduct.Order = order;
                orderProduct.Product = product;
                _context.OrderProducts.Add(orderProduct);
                await _context.SaveChangesAsync();
            } 
            
            response.Data = "Created new order";
            return response;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders(int id)
        {
            var dbOrders = await _context.Orders.ToListAsync();
            var response = new ServiceResponse<List<GetOrderDto>>();
            var orders = dbOrders.Select(c => _mapper.Map<GetOrderDto>(c)).ToList();
            response.Data = orders;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetOrder(int orderId)
        {
            var dbOrders = await _context.OrderProducts.Include(x => x.Order).Include(x => x.Product.Supplier).ToListAsync();
            var orders = dbOrders.Where(x => x.Order.Id == orderId);

            var products = new List<GetProductDto>();
            foreach(var order in orders)
            {
                //var product = _context.Products.Include(x => x.Supplier).FirstOrDefault(x => x.Id == order.Product.Id);
                var product = order.Product;
                var productDto = _mapper.Map<GetProductDto>(product);
                products.Add(productDto);
            }

            var response = new ServiceResponse<List<GetProductDto>>();
            response.Data = products;
            return response;
        }

        public async Task<ServiceResponse<List<GetUserOrderDto>>> GetMyUserOrders(int userId)
        {
            var response = new ServiceResponse<List<GetUserOrderDto>>();
            var orderList = new List<GetUserOrderDto>();
            var dbOrders = await _context.Orders.Include(x => x.User).Where(x => x.User.Id == userId).ToListAsync();

            foreach(var order in dbOrders) {
                var getOrderDto = new GetUserOrderDto();
                getOrderDto.Id = order.Id;
                getOrderDto.UserId = userId;
                getOrderDto.UserName = order.User.Name + " " + order.User.Surname;
                getOrderDto.Products = (await GetOrder(order.Id)).Data;
                orderList.Add(getOrderDto);
            }

            response.Success = true;
            response.Data = orderList;
            return response;
        }

        public async Task<ServiceResponse<List<GetUserOrderDto>>> GetMySupplierOrders(int userId)
        {
            var response = new ServiceResponse<List<GetUserOrderDto>>();
            var orderList = new List<GetUserOrderDto>();
            var dbOrders = await _context.OrderProducts.Include(x => x.Order.User).Include(x => x.Product.Supplier).Where(x => x.Product.Supplier.Id == userId).ToListAsync();
            foreach(var order in dbOrders.Select(x => x.Order).Distinct()) {
                var getOrderDto = new GetUserOrderDto();
                getOrderDto.Products = new List<GetProductDto>();
                getOrderDto.UserName = order.User.Name + " " + order.User.Surname;
                getOrderDto.Id = order.Id;
                getOrderDto.UserId = order.User.Id;
                var dbOrderProducts = dbOrders.Where(x => x.Order.Id == order.Id).Select(x => x.Product).ToList();
                getOrderDto.Products.AddRange(dbOrderProducts.Select(x => _mapper.Map<GetProductDto>(x)).ToList());
                orderList.Add(getOrderDto);
            }
            response.Success = true;
            response.Data = orderList;
            return response;
        }
    }
}