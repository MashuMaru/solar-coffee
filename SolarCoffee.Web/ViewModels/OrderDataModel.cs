using System;
using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Web.ViewModels
{
    public class OrderDataModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CustomerDataModel Customer { get; set; }
        public List<SalesOrderItemDataModel> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}