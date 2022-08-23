using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data
{
    public class SolarDbContext : IdentityDbContext
    {
        public SolarDbContext() { }
        public SolarDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<CustomerDataModel>Customers { get; set; }
        public virtual DbSet<CustomerAddressDataModel>CustomerAddresses { get; set; }
        public virtual DbSet<ProductDataModel>Products { get; set; }
        public virtual DbSet<ProductInventoryDataModel> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySnapshotDataModel>ProductInventorySnapshots { get; set; }
        public virtual DbSet<SalesOrderDataModel>SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItemDataModel>SalesOrderItems { get; set; }
    }
}