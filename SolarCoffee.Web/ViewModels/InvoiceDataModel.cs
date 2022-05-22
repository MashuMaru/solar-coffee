using System;
using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Web.ViewModels
{
    public class InvoiceDataModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemDataModel> LineItems { get; set; }
    }
}