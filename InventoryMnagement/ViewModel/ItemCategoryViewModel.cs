using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMnagement.ViewModel
{
    public class ItemCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
