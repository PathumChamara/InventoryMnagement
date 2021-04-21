using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMnagement.Models
{
    public class Transaction : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }


        [Required]
        public int Quantity { get; set; }
    }
}
