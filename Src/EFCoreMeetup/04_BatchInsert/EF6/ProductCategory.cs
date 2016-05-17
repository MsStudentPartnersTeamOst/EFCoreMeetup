using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace _04_BatchInsert.EF6
{
    [Table("ProductCategory", Schema = "Production")]
    [DebuggerDisplay("{ProductCategoryID} - {Name}")]
    internal class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }

        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
