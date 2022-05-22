using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class OrderMapper
    {
        public static SalesOrder SerialiseInvoiceToOrder(InvoiceDataModel invoice)
        {
            var salesOrderItems = invoice.LineItems
                .Select(item => new SalesOrderItem {
                    Id = item.Id,
                    Quantity = item.Quantity, 
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();
            
            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
        
        public static List<OrderDataModel> SerialisingOrdersToViewModels (IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderDataModel {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerialisesSalesOrderItem(order.SalesOrderItems),
                Customer = CustomerMapper.SerialisesCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }

        private static List<SalesOrderItemDataModel> SerialisesSalesOrderItem(List<SalesOrderItem> salesOrderItems)
        {
            return salesOrderItems.Select(item => new SalesOrderItemDataModel {
                Id = item.Id,
                Quantity = item.Quantity, 
                Product = ProductMapper.SerializeProductModel(item.Product),
            }).ToList();
        }
    }
}