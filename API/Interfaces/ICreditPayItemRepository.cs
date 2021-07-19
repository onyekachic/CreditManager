using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ICreditPayItemRepository
    {
        void Add(CreditPayItem creditPayItem);
        void Delete(CreditPayItem creditPayItem);
        void Update(CreditPayItem creditPayItem);
        Task<CreditPayItem>  GetCreditPayItemByIdAsync(int Id);
        Task<IEnumerable<CreditPayItem>> GetCreditPayItemsAsync();
         Task<PagedList<CreditPayItemDto>> GetCreditsAsync(CreditPayItemParams creditPayItemParams);
        Task<System.Object> GetCreditPayItemAsync(int Id);
    }
}