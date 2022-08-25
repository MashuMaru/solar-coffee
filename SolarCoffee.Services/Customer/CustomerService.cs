using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _db;

        public CustomerService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }
        
        public ServiceResponse<List<CustomerDataModel>> GetAllCustomers()
        {
            try 
            {
                var result = _db.Customers.Include(customer => customer.PrimaryAddress).OrderBy(customer => customer.LastName).ToList();
                return new ServiceResponse<List<CustomerDataModel>>()
                {
                    Data = result,
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<List<CustomerDataModel>>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ServiceResponse<CustomerDataModel> CreateCustomer(CustomerDataModel customer)
        {
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<CustomerDataModel>()
                {
                    IsSuccess = true,
                    Message = "Customer added to the database.",
                    Data = customer,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<CustomerDataModel>()
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
            var customer = _db.Customers.Find(customerId);
            if (customer == null)
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
                _db.Customers.Remove(customer);
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

        public CustomerDataModel GetCustomerById(int customerId)
        {
            return _db.Customers.Find(customerId);
        }
  }
}