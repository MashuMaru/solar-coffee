using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class ProductMapper
    {
        public static ProductModel SerializeProductModel(ProductDataModel product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.CreatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
        
        public static ProductDataModel SerializeProductModel(ProductModel product)
        {
            return new ProductDataModel
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.CreatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }
}