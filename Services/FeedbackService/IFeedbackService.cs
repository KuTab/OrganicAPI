using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Services.FeedbackService
{
    public interface IFeedbackService
    {
        public Task<ServiceResponse<String>> SendSupplierFeedback(AddSupplierFeedbackDto feedback);

        public Task<ServiceResponse<String>> SendProductFeedback(AddProductFeedbackDto feedback);

        public Task<ServiceResponse<List<SupplierFeedbackDto>>> GetSupplierFeedback(int id);

        public Task<ServiceResponse<List<ProductFeedbackDto>>> GetProductFeedback(int id);

    }
}