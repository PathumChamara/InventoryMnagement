using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMnagement.ViewModel
{
    public class TransactionForm
    {
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
