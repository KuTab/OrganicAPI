using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.FeedbackService
{
    public class FeedbackService: IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public FeedbackService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ProductFeedbackDto>>> GetProductFeedback(int id)
        {
            var dBFeedback = _context.ProductFeedbacks.Include(x => x.Product).Where(x => x.Product.Id == id);
            var feedback = dBFeedback.Select(x => _mapper.Map<ProductFeedbackDto>(x)).ToList();
            var response = new ServiceResponse<List<ProductFeedbackDto>>();
            response.Success = true;
            response.Data = feedback;
            return response;
        }

        public async Task<ServiceResponse<List<SupplierFeedbackDto>>> GetSupplierFeedback(int id)
        {
            var dBFeedback = _context.SupplierFeedbacks.Include(x => x.Supplier).Where(x => x.Supplier.Id == id);
            var feedback = dBFeedback.Select(x => _mapper.Map<SupplierFeedbackDto>(x)).ToList();
            var response = new ServiceResponse<List<SupplierFeedbackDto>>();
            response.Success = true;
            response.Data = feedback;
            return response;
        }

        public async Task<ServiceResponse<string>> SendProductFeedback(AddProductFeedbackDto feedback)
        {
            var productFeedback = _mapper.Map<ProductFeedback>(feedback);
            var product = _context.Products.First(x => x.Id == feedback.ProductID);
            productFeedback.Product = product;
            _context.ProductFeedbacks.Add(productFeedback);
            await _context.SaveChangesAsync();
            await UpdateProductRating(product.Id);
            var response = new ServiceResponse<string>();
            response.Success = true;
            response.Data = "Отзыв был успешно отправлен";
            return response;
        }

        public async Task<ServiceResponse<string>> SendSupplierFeedback(AddSupplierFeedbackDto feedback)
        {
            var supplierFeedback = _mapper.Map<SupplierFeedback>(feedback);
            var user = _context.Users.First(x => x.Id == feedback.SupplierId);
            supplierFeedback.Supplier = user;
            _context.SupplierFeedbacks.Add(supplierFeedback);
            await _context.SaveChangesAsync();
            await UpdateSupplierRating(user.Id);
            var response = new ServiceResponse<string>();
            response.Success = true;
            response.Data = "Отзыв был успешно отправлен";
            return response;
        }

        private async Task UpdateProductRating(int productId) {
            var feedbacks = await _context.ProductFeedbacks.Include(x => x.Product).Where(x => x.Product.Id == productId).ToListAsync();
            var amount = feedbacks.Count;
            var sum = 0.0;
            if (amount == 0) {
                return;
            }
            foreach(var feedback in feedbacks) {
                sum += feedback.Rate;
            }
            var newRating = sum/amount;
            var product = _context.Products.First(x => x.Id == productId);
            product.Rating = newRating;
            await _context.SaveChangesAsync();
        }

        private async Task UpdateSupplierRating(int supplierId) {
            var feedbacks = await _context.SupplierFeedbacks.Include(x => x.Supplier).Where(x => x.Supplier.Id == supplierId).ToListAsync();
            var amount = feedbacks.Count;
            var sum = 0.0;
            if (amount == 0) {
                return;
            }
            foreach(var feedback in feedbacks) {
                sum += feedback.Rate;
            }
            var newRating = sum/amount;
            var supplier = _context.Users.First(x => x.Id == supplierId);
            supplier.Rating = newRating;
            await _context.SaveChangesAsync();
        }
    }
}