using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }
        
        public List<ProductInventoryDataModel> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        public ServiceResponse<ProductInventoryDataModel> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);
                
                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapShot(inventory);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                }
                
                _db.SaveChanges();

                return new ServiceResponse<ProductInventoryDataModel>()
                {
                    Data = inventory,
                    IsSuccess = true,
                    Message = "Updated Quantity on Hand for specified product id.",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventoryDataModel>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ProductInventoryDataModel GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .First(pi => pi.Product.Id == productId);
        }

        private void CreateSnapShot(ProductInventoryDataModel productInventory)
        {
            var snapshot = new ProductInventorySnapshotDataModel()
            {
                Id = productInventory.Id,
                Product = productInventory.Product,
                QuantityOnHand = productInventory.QuantityOnHand,
                SnapShotTime = DateTime.UtcNow
            };
            _db.ProductInventorySnapshots.Add(snapshot);
        }

        public List<ProductInventorySnapshotDataModel> GetSnapShotHistory()
        {
            // TODO: Let user determine how many hours ago. /snap-shot-history/{q?}
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap
                    => snap.SnapShotTime > earliest &&
                       !snap.Product.IsArchived)
                .ToList();
        }
    }
}