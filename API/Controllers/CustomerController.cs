using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class CustomerController : BaseApiController
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;

    }

  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers([FromQuery] CustomerParams customerParams)
    {
      
        var customers = await _unitOfWork.CustomerRepository.GetCustomers(customerParams);

        Response.AddPaginationHeader(customers.CurrentPage, customers.PageSize,
            customers.TotalCount, customers.TotalPages);

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<System.Object>> GetCustomerById(int Id)
    {
      var customer = await _unitOfWork.CustomerRepository.GetCustomerAsync(Id);

      return Ok(new {customer});

    }

    [HttpPost]
    public async Task<ActionResult> PostCustomer(Customer customer)
    {
      if(customer.CustomerID == 0)
      _unitOfWork.CustomerRepository.Add(customer);
      else 
      {
        _unitOfWork.CustomerRepository.Update(customer);
      }

    if (await _unitOfWork.Complete()) return Ok();

      return BadRequest("Failed to save Customer");
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int Id)
    {
       _unitOfWork.CustomerRepository.DeleteById(Id);

       if (await _unitOfWork.Complete()) return Ok();

      return BadRequest("Failed to Delete Customer");

    }
  }
}