using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Order
{
    public interface IOrderService
    {
        List<SalesOrderDataModel> GetOrders();
        ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrderDataModel order);
        ServiceResponse<bool> MarkFulfilled(int orderId);
    }
}