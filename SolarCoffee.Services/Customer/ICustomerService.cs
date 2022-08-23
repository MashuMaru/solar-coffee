using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Customer
{
    public interface ICustomerService
    {
        ServiceResponse<List<CustomerDataModel>> GetAllCustomers();
        ServiceResponse<CustomerDataModel> CreateCustomer(CustomerDataModel customer);
        ServiceResponse<bool> DeleteCustomer(int customerId);
        CustomerDataModel GetCustomerById(int customerId);
    }
}