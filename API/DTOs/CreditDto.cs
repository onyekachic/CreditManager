using System;
using API.Entities;

namespace API.DTOs
{
    public class CreditDto
    {
       
        public int CreditID { get; set; }
        public string CreditNo { get; set; }
        public decimal Amount { get; set; }
         public decimal GTotal { get; set; }
         public string GroupName { get; set; }
        public int CustomerID { get; set; }
        public Customer  Customer { get; set; }
       
        
    }
}