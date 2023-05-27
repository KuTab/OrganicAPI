using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Dtos.Feedback
{
    public class SupplierFeedbackDto
    {
        public string Feedback { get; set; }
        public int Rate { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}