using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
  [ApiController]
  [Route("api/")]
  public class CustomerController : ControllerBase
  {
    public CustomerController(
      ILogger<CustomerController> logger,
      ICustomerService customerService)
    {
      _logger = logger;
      _customerService = customerService;
    }

    [HttpPost("create-customer")]
    public ActionResult CreateCustomer ([FromBody] CustomerModel model)
    {
      var customerData = CustomerMapper.SerialiseCustomer(model);
      var newCustomer = _customerService.CreateCustomer(customerData);

      return Ok(newCustomer);
    }

    [HttpGet("customers")]
    public ActionResult GetCustomers ()
    {
      _logger.LogInformation("Getting customers");
      var result = _customerService.GetAllCustomers();

      if (!result.IsSuccess) 
      {
        return BadRequest(result.Message);
      }

      var customerModels = result.Data.Select(customer => new CustomerModel
      {
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddress),
        CreatedOn = customer.CreatedOn,
        UpdatedOn = customer.UpdatedOn
      })
      .OrderBy(customer => customer.CreatedOn)
      .ToList();

      return Ok(customerModels);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteCustomer(int id)
    {
      _logger.LogInformation($"Deleting customer ${id}");
      var response = _customerService.DeleteCustomer(id);

      return Ok(response);
    }

    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;
  }
}