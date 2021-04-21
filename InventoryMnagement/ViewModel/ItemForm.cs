using InventoryMnagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryMnagement.ViewModel
{
    public class ItemForm // Post/Update
    {
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

    }

    public static class ItemViewModel // get
    {

        public static Expression<Func<Item, object>> SelectAllItem =>
            x => new
            {
                Name = x.ItemName,
                ThresholdEnable = x.ThresholdEnable,
                CreatedUser = x.CreatedUser,
                Quantity = x.Quantity,
            };
        public static Expression<Func<Item, object>> SelectById =>
            x => new
            {
                Name = x.ItemName,
                CategoryName = x.ItemCategory.Category
            };
    }

}
