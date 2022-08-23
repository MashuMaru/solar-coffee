using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;

        public ProductService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }
        
        public List<ProductDataModel> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public ProductDataModel GetProductById(int id)
        {
            var productById = _db.Products.Find(id);
            if (productById == null)
            {
                throw new Exception("No product found using that id.");
            }
            
            return productById;
        }
        
        public ServiceResponse<ProductDataModel> CreateProduct(ProductDataModel product)
        {
            try
            {
                _db.Products.Add(product);
                var newInventory = new ProductInventoryDataModel
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };
                _db.ProductInventories.Add(newInventory);
                _db.SaveChanges();

                return new ServiceResponse<ProductDataModel>
                {
                    Data = product,
                    Time = DateTime.Now.ToLocalTime(),
                    IsSuccess = true,
                    Message = "Successfully saved new product."
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductDataModel>
                {
                    Data = product,
                    Time = DateTime.Now.ToLocalTime(),
                    IsSuccess = false,
                    Message = e.StackTrace
                };
            }
        }

        public ServiceResponse<ProductDataModel> ArchiveProduct(int id)
        {
            try
            {
                var productToArchive = _db.Products.Find(id);
                productToArchive.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<ProductDataModel>
                {
                    Data = productToArchive,
                    Time = DateTime.Now.ToLocalTime(),
                    IsSuccess = true,
                    Message = "Successfully archived product."
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductDataModel>
                {
                    Data = null,
                    Time = DateTime.Now.ToLocalTime(),
                    IsSuccess = false,
                    Message = e.StackTrace
                };
            }
        }
    }
}