using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class OrderMapper
    {
        public static SalesOrderDataModel SerialiseInvoiceToOrder(InvoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems
                .Select(item => new SalesOrderItemDataModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity, 
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();
            
            return new SalesOrderDataModel
            {
                SalesOrderItems = salesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
        
        public static List<OrderModel> SerialisingOrdersToViewModels (List<SalesOrderDataModel> orders)
        {
            return orders.Select(order => new OrderModel 
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerialisesSalesOrderItem(order.SalesOrderItems),
                Customer = CustomerMapper.SerialisesCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }

        private static List<SalesOrderItemModel> SerialisesSalesOrderItem(List<SalesOrderItemDataModel> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel 
            {
                Id = item.Id,
                Quantity = item.Quantity, 
                Product = ProductMapper.SerializeProductModel(item.Product),
            }).ToList();
        }
    }
}