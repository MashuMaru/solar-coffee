using System;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public class CustomerMapper
    {
        public static CustomerModel SerialisesCustomer(CustomerDataModel customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        public static CustomerDataModel SerialiseCustomer(CustomerModel customer)
        {
            return new CustomerDataModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddressData(customer.PrimaryAddress)
            };
        }

        public static CustomerAddressModel MapCustomerAddress(CustomerAddressDataModel address)
        {
            return new CustomerAddressModel
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostCode = address.PostCode,
                Country = address.Country,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }

         public static CustomerAddressDataModel MapCustomerAddressData(CustomerAddressModel address)
        {
            return new CustomerAddressDataModel
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostCode = address.PostCode,
                Country = address.Country,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
    }
}