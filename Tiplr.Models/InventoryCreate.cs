using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiplr.Models
{
    public class InventoryCreate
    {
        public int InventoryId { get; set; }
        [Display(Name = "Inventory Date")]
        public DateTimeOffset InventoryDate { get; set; }

    }
}
