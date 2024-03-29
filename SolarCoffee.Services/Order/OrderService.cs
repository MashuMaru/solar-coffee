using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(
            SolarDbContext db,
            ILogger<OrderService> logger,
            IProductService productService,
            IInventoryService inventoryService)
        {
            _db = db;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }
        
        public List<SalesOrderDataModel> GetOrders()
        {
            return _db.SalesOrders
                .Include(salesOrder => salesOrder.Customer)
                    .ThenInclude(salesOrder => salesOrder.PrimaryAddress)
                .Include(salesOrder => salesOrder.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .ToList();
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrderDataModel order)
        {
            _logger.LogInformation("Generating new Open Order");

            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService
                    .GetProductById(item.Product.Id);
                var inventoryId = _inventoryService
                    .GetByProductId(item.Product.Id).Id;
                _inventoryService
                    .UpdateUnitsAvailable(inventoryId, -item.Quantity);
            };
            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Successfully created Sales Order.",
                    Time = DateTime.UtcNow,
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ServiceResponse<bool> MarkFulfilled(int orderId)
        {
            var order = _db.SalesOrders.Find(orderId);
            order.UpdatedOn = DateTime.UtcNow;
            order.IsPaid = true;

            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Time = DateTime.UtcNow,
                    Message = $"Marked order as fulfilled for {order.Id}",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }
    }
}