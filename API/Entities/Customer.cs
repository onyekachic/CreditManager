using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string GroupName { get; set; }
        public ICollection<Credit> Credit { get; set; }
    }
}