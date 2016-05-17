using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace _02_Performance.EFCore
{
    [Table("Customer", Schema = "Sales")]
    [DebuggerDisplay("{CustomerID} - {AccountNumber}")]
    public class Customer
    {
        //[Key]
        public int CustomerID { get; set; }

        public int? PersonID { get; set; }

        public int? StoreID { get; set; }

        public int? TerritoryID { get; set; }

        public string AccountNumber { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
