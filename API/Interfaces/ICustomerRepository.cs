using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Delete(Customer customer);
        void DeleteById(int Id);
        void Update(Customer customer);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<PagedList<CustomerDto>> GetCustomers(CustomerParams customerParams);
        Task<System.Object> GetCustomerAsync(int Id);
    }
}