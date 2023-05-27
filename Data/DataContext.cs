using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
        public DbSet<SupplierFeedback> SupplierFeedbacks => Set<SupplierFeedback>();
        public DbSet<ProductFeedback> ProductFeedbacks => Set<ProductFeedback>();
        public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    }
}