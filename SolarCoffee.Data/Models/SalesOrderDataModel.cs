using System;
using System.Collections.Generic;

namespace SolarCoffee.Data.Models
{
    public class SalesOrderDataModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CustomerDataModel Customer { get; set; }
        public List<SalesOrderItemDataModel> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}