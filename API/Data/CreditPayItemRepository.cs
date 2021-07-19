using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class CreditPayItemRepository: ICreditPayItemRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CreditPayItemRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;

    }
    public void Add(CreditPayItem creditPayItem)
    {
      _context.CreditPayItems.Add(creditPayItem);
    }

    public void Delete(CreditPayItem creditPayItem)
    {
      _context.CreditPayItems.Remove(creditPayItem);
    }

    public void Update(CreditPayItem creditPayItem)
    {
      _context.Entry(creditPayItem).State = EntityState.Modified;
    }

    public async Task<System.Object> GetCreditPayItemAsync(int Id)
    {
      var creditPayItems = (from a in _context.CreditPayItems
                       where a.Credit.CreditID == Id
                       select new
                       {
                        a.CreditID,
                        a.CreditPayItemID,
                        a.RepayAmt,
                        a.Pension,
                        a.School,
                        a.Union,
                        a.Others,
                        Total = a.RepayAmt + a.Pension + a.School + a.Union + a.Others
                       }).ToListAsync();

      return await creditPayItems;  

    }
      public async Task<CreditPayItem> GetCreditPayItemByIdAsync(int Id)
    {
      var creditPayItem =  _context.CreditPayItems.FindAsync(Id);
      return await creditPayItem;
    }
  public async Task<IEnumerable<CreditPayItem>> GetCreditPayItemsAsync()
  {
    return await _context.CreditPayItems
    .ToListAsync();
  }
  public async Task<PagedList<CreditPayItemDto>> GetCreditsAsync(CreditPayItemParams creditPayItemParams)
  {
      var query = _context.CreditPayItems
          .ProjectTo<CreditPayItemDto>(_mapper.ConfigurationProvider)
          .AsQueryable();

      return await PagedList<CreditPayItemDto>.CreateAsync(query, creditPayItemParams.PageNumber, creditPayItemParams.PageSize);
  }

  }
}