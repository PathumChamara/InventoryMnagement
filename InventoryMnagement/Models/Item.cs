using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMnagement.Models
{
    public class Item : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool ThresholdEnable { get; set; }
        [Required]
        public int MinThreshold { get; set; }
        [Required]
        public int ItemCategoryId { get; set; }

        public ItemCategory ItemCategory { get; set; }


    }
}
