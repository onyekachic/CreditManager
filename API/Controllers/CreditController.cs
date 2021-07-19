using System;
using System.Collections.Generic;
using System.Linq;
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
  public class CreditController : BaseApiController
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreditController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;

    }
    
     //[HttpGet]
    // public async Task<ActionResult<System.Object>> GetCredits()
    // {
    //     return await _unitOfWork.CreditRepository.GetCreditsAsync();
    // }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreditDto>>> GetCreditsAsync([FromQuery] CreditParams creditParams)
    {
        var credits = await _unitOfWork.CreditRepository.GetCreditsAsync(creditParams);

        Response.AddPaginationHeader(credits.CurrentPage, credits.PageSize,
            credits.TotalCount, credits.TotalPages);

        return Ok(credits);
    }
   
   [HttpGet("{id}")]
    public async Task<ActionResult<System.Object>> GetCreditById(int Id)
    {
      var credit = await _unitOfWork.CreditRepository.GetCreditAsync(Id);

      var creditDetails = await _unitOfWork.CreditPayItemRepository.GetCreditPayItemAsync(Id);

      return Ok(new {credit, creditDetails});

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCredit(int Id)
    {
       _unitOfWork.CreditRepository.DeleteCreditById(Id);

       if (await _unitOfWork.Complete()) return Ok();

      return BadRequest("Failed to Delete Credit");

    }

     [HttpPost]
    public async Task<ActionResult> PostCredit(Credit credit)
    {
      if(credit.CreditID == 0)
      _unitOfWork.CreditRepository.AddCredit(credit);
      else 
      {
        _unitOfWork.CreditRepository.UpdateCredit(credit);
      }

      foreach (var item in credit.CreditPayItems)
      {
        if(item.CreditPayItemID == 0)
          _unitOfWork.CreditPayItemRepository.Add(item);
          else
          {
            _unitOfWork.CreditPayItemRepository.Update(item);
          }
      }

      ///Delete Operation for CreditPayItem

      foreach (var id in credit.DeletedCreditPayItemIDs.Split(',').Where(x => x!= ""))
      {
          CreditPayItem x = await _unitOfWork.CreditPayItemRepository.GetCreditPayItemByIdAsync(Convert.ToInt16(id));
          _unitOfWork.CreditPayItemRepository.Delete(x);
          
      }

      if (await _unitOfWork.Complete()) return Ok();

      return BadRequest("Failed to save Client");
    }
  }
}