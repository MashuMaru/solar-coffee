using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        List<ProductInventoryDataModel> GetCurrentInventory();
        ServiceResponse<ProductInventoryDataModel> UpdateUnitsAvailable(int id, int adjustment);
        ProductInventoryDataModel GetByProductId(int productId);
        List<ProductInventorySnapshotDataModel> GetSnapShotHistory();
    }
}