using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddProductDto, Product>();
            CreateMap<User, GetUserDto>();
            CreateMap<Order, GetOrderDto>();
            CreateMap<Product, GetProductDto>();
            CreateMap<ProductFeedback, ProductFeedbackDto>();
            CreateMap<SupplierFeedback, SupplierFeedbackDto>();
            CreateMap<AddSupplierFeedbackDto, SupplierFeedback>();
            CreateMap<AddProductFeedbackDto, ProductFeedback>();
        }
    }
}