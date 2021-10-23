using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiplr.Data
{
    public class Product
    { 
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [ForeignKey(nameof(ProductCategory))]
        public int? CategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        [Required]
        public string CountBy { get; set; }
        [Required]
        public string OrderBy { get; set; }
        [Required]
        public int UnitsPerPack { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public int Par { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset CreatedDtTm { get; set; }
        public DateTimeOffset LastModifiedDtTm { get; set; }
        public DateTimeOffset InactiveDtTm { get; set; }
        public Guid UpdtUser { get; set; }

    }
}
