using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Composition { get; set; }
        public string Expiration { get; set; }
        public string StorageCondition { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public User Supplier { get; set; }
    }
}