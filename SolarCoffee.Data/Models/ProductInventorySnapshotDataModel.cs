using System;

namespace SolarCoffee.Data.Models
{
    public class ProductInventorySnapshotDataModel
    {
        public int Id { get; set; }
        public DateTime SnapShotTime { get; set; }
        public int QuantityOnHand { get; set; }
        public ProductDataModel Product { get; set; }
    }
}