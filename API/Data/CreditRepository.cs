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
  public class CreditRepository: ICreditRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CreditRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;

    }
    public void AddCredit(Credit credit)
    {
      _context.Credits.Add(credit);
    }

    public void DeleteCredit(Credit credit)
    {
      _context.Credits.Remove(credit);
    }

    public void DeleteCreditById(int Id)
    {
      Credit credit = _context.Credits
              .Include(y => y.CreditPayItems)
              .SingleOrDefault(x => x.CreditID == Id);
      foreach (var item in credit.CreditPayItems.ToList())
      {
        _context.CreditPayItems.Remove(item);
      }

      _context.Credits.Remove(credit);
    }

    public void UpdateCredit(Credit credit)
    {
      _context.Entry(credit).State = EntityState.Modified;
    }

    public async Task<System.Object> GetCreditAsync(int Id)
    {
      ;
      var credit = (from a in _context.Credits
                   where a.CreditID  == Id
                   select new
                   {
                     a.CreditID,
                     a.CreditNo,
                     a.CustomerID,
                     a.GroupName,
                     a.Amount,
                     a.GTotal,
                     DeletedCreditPayItemIDs = "",
                   }).FirstOrDefaultAsync();

      return await credit;


    }
    public async Task<System.Object> GetCreditsAsync()
    {
      var results = (from a in _context.Credits
                     join b in _context.Customers on a.CustomerID equals b.CustomerID

                     select new
                     {
                       a.CreditID,
                       a.CreditNo,
                       Customer = b.ContactName,
                       a.GroupName,
                       a.Amount,
                       a.GTotal
                     }).ToListAsync();
      return await results;
    }

    public async Task<PagedList<CreditDto>> GetCreditsAsync(CreditParams creditParams)
    {
        var query = _context.Credits
            .ProjectTo<CreditDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

        return await PagedList<CreditDto>.CreateAsync(query, creditParams.PageNumber, creditParams.PageSize);

    }
     
  }

   
}