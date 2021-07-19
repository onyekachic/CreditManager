using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class CustomerRespository : ICustomerRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CustomerRespository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;

    }

      public void Add(Customer customer)
    {
      _context.Customers.Add(customer);
    }

    public void Delete(Customer customer)
    {
      _context.Customers.Remove(customer);
    }

     public void DeleteById(int Id)
    {
      Customer customer = _context.Customers
                    .SingleOrDefault(x => x.CustomerID == Id);
  
      _context.Customers.Remove(customer);             
    }

    public void Update(Customer customer)
    {
      _context.Entry(customer).State = EntityState.Modified;
    }

    public async Task<System.Object> GetCustomerAsync(int Id)
    {
      var customer = (from a in _context.Customers
                  where a.CustomerID == Id
                  select new 
                  {

                    a.CustomerID,
                    a.ContactName,
                    a.Address,
                    a.Phone,
                    a.GroupName
                    
                  }).FirstOrDefaultAsync();

         return await customer;       

     
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
      return await _context.Customers
      .ToListAsync();
    }

      public async Task<PagedList<CustomerDto>> GetCustomers(CustomerParams customerParams)
        {
            var query = _context.Customers
                .OrderByDescending(c => c.ContactName)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            // query = query.Where(c => c.ContactName == customerParams.Customername);
           // query = query.Where(c => c.GroupName == customerParams.GroupName);    

            return await PagedList<CustomerDto>.CreateAsync(query, customerParams.PageNumber, customerParams.PageSize);

        }

  }
}