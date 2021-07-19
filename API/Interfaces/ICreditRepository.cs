using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ICreditRepository
    {
        void AddCredit(Credit credit);
        void DeleteCredit(Credit credit);
        void DeleteCreditById(int Id);
        void UpdateCredit(Credit credit);
        Task<System.Object> GetCreditsAsync();
        Task<PagedList<CreditDto>> GetCreditsAsync(CreditParams creditParams);
        Task<System.Object> GetCreditAsync(int Id);
    }
}