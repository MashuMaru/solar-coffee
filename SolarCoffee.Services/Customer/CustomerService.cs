using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _db;

        public CustomerService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }
        
        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();
        }

        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>()
                {
                    IsSuccess = true,
                    Message = "Customer added to the database.",
                    Data = customer,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int customerId)
        {
            var deleteCustomer = _db.Customers.Find(customerId);
            if (deleteCustomer == null)
            {
                return new ServiceResponse<bool>()
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = "Customer does not exist.",
                    Data = false
                };
            }
            try
            {
                _db.Customers.Remove(deleteCustomer);
                _db.SaveChanges();
                return new ServiceResponse<bool>()
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Successfully removed customer from the database.",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public Data.Models.Customer GetCustomerById(int customerId)
        {
            return _db.Customers.Find(customerId);
        }
    }
}