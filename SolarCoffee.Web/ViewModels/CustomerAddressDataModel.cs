using System;
using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.Web.ViewModels
{
    public class CustomerAddressDataModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        [MaxLength(100)]
        public string AddressLine1 { get; set; }
        
        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        public string State { get; set; }
        
        [MaxLength(100)]
        public string City { get; set; }
        
        [MaxLength(10)]
        public string PostCode { get; set; }
        
        [MaxLength(64)]
        public string Country { get; set; }
    }
}