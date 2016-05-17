using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Performance.EF6
{
    class Customer
    {
        public int CustomerID { get; set; }
        public int? PersonID { get; set; }

        public int? StoreId { get; set; }

        public int? TerritoryID { get; set; }

        public string AccountNumber { get; set; }

        public Guid rowguid { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
