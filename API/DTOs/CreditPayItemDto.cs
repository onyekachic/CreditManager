using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class CreditPayItemDto
    {
        
        public int CreditPayItemID { get; set; }
        public decimal  RepayAmt { get; set; }
        public decimal  Pension { get; set; }
        public decimal  Union { get; set; }
        public decimal  School { get; set; }
        public decimal  Others { get; set; }
        public DateTime Created { get; set; } 
        public int CreditID { get; set; }
        public Credit  Credit { get; set; }
    }
}