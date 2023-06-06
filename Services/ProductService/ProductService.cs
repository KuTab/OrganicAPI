using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

         public ProductService(IMapper mapper, DataContext context)
         {
            _context = context;
            _mapper = mapper;
         }

        public async Task<ServiceResponse<string>> AddProduct(AddProductDto newProduct, int userId)
        {
            var product = _mapper.Map<Product>(newProduct);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            product.Supplier = user;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            var response = new ServiceResponse<String>();
            response.Data = "Successfuly added new product";
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<string>> AddQuantity(int supplierId, int productId)
        {
            var response = new ServiceResponse<string>();
            var product = _context.Products.Include(x => x.Supplier).First(x => x.Id == productId);
            if (product == null) {
                response.Success = false;
                response.Data = "Нет продукта с таким id";
                return response;
            }
            if (supplierId == product.Supplier.Id) {
                product.Quantity += 1;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Data = "Уcпешно добавлено количество продукта";
                return response;
            } else {
                response.Success = false;
                response.Data = "Этот продукт не пренадлежит пользователю";
                return response;
            }
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            var dbProducts = await _context.Products.Include(x => x.Supplier).ToListAsync();
            var response = new ServiceResponse<List<GetProductDto>>();
            response.Success = true;
            var products = dbProducts.Select(c => _mapper.Map<GetProductDto>(c)).ToList();
            response.Data = products;
            return response;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetMyProducts(int userId) {
            var dbProducts = await _context.Products.Include(x => x.Supplier).ToListAsync();
            var myProducts =  dbProducts.Where(x => x.Supplier.Id == userId).ToList();
            var mappedProducts = myProducts.Select(x => _mapper.Map<GetProductDto>(x)).ToList();
            var response = new ServiceResponse<List<GetProductDto>>();
            response.Data = mappedProducts;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetProductsByCategory(string category)
        {
            var dbProducts = await _context.Products.Include(x => x.Supplier).Where(x => x.Category == category && x.Quantity > 0).ToListAsync();
            var response = new ServiceResponse<List<GetProductDto>>();
            var mappedProducts = dbProducts.Select(x => _mapper.Map<GetProductDto>(x)).ToList();
            response.Success = true;
            response.Data = mappedProducts;
            return response;
        }
    }
}