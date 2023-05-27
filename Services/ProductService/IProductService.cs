using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.ProductService
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();

        public Task<ServiceResponse<String>> AddProduct(AddProductDto newProduct, int userId);

        public Task<ServiceResponse<List<GetProductDto>>> GetMyProducts(int userId);

        public Task<ServiceResponse<List<GetProductDto>>> GetProductsByCategory(string category);

        public Task<ServiceResponse<string>> AddQuantity(int supplierId, int productId);
    }
}