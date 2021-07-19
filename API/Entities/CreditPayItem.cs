using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class CreditPayItem
    {
        [Key]
        public int CreditPayItemID { get; set; }
        public Nullable<decimal>  RepayAmt { get; set; }
        public Nullable<decimal>  Pension { get; set; }
        public Nullable<decimal>  Union { get; set; }
        public Nullable<decimal>  School { get; set; }
        public Nullable<decimal>  Others { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int CreditID { get; set; }
        public virtual Credit  Credit { get; set; }
    }
}