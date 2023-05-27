using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Model
{
    public class ProductFeedback
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Feedback { get; set; }
        public int Rate { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}