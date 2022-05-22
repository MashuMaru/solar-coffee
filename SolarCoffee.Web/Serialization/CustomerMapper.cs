using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public class CustomerMapper
    {
        public static CustomerDataModel SerialisesCustomer(Customer customer)
        {
            var address = new CustomerAddressDataModel
            {
                Id = customer.Id,
                AddressLine1 = customer.PrimaryAddress.AddressLine1,
                AddressLine2 = customer.PrimaryAddress.AddressLine2,
                City = customer.PrimaryAddress.City,
                State = customer.PrimaryAddress.State,
                Country = customer.PrimaryAddress.Country,
                PostCode = customer.PrimaryAddress.PostCode,
                CreatedOn = customer.PrimaryAddress.CreatedOn,
                UpdatedOn = customer.PrimaryAddress.UpdatedOn
            };

            return new CustomerDataModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = address
            };
        }

        public static Customer SerialiseCustomer(CustomerDataModel customer)
        {
            var address = new CustomerAddress
            {
                Id = customer.Id,
                AddressLine1 = customer.PrimaryAddress.AddressLine1,
                AddressLine2 = customer.PrimaryAddress.AddressLine2,
                City = customer.PrimaryAddress.City,
                State = customer.PrimaryAddress.State,
                Country = customer.PrimaryAddress.Country,
                PostCode = customer.PrimaryAddress.PostCode,
                CreatedOn = customer.PrimaryAddress.CreatedOn,
                UpdatedOn = customer.PrimaryAddress.UpdatedOn
            };

            // clear seperation between the View models and models.
            // TODO: CustomerModel (to)
            // TODO: CustomerDataModel (from)

            return new Customer
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = address
            };
        }
    }
}