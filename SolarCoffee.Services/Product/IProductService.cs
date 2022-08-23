using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Product
{
    public interface IProductService
    {
        List<ProductDataModel> GetAllProducts();
        ProductDataModel GetProductById(int id);
        ServiceResponse<ProductDataModel> CreateProduct(ProductDataModel product);
        ServiceResponse<ProductDataModel> ArchiveProduct(int id);
    }
}