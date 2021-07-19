using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Credit
    {
        [Key]
        public int CreditID { get; set; }
        public string CreditNo { get; set; }
        public decimal Amount { get; set; }
         public Nullable<decimal> GTotal { get; set; }
         public string GroupName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [NotMapped]
        public string DeletedCreditPayItemIDs { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer  Customer { get; set; }
       public ICollection<CreditPayItem> CreditPayItems { get; set; }
    }
}