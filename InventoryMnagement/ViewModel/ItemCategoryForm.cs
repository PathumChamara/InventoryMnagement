using InventoryMnagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryMnagement.ViewModel
{
    public class ItemCategoryForm
    {
        public int Id { get; set; }
        [Required]
        public string Category { get; set; }
    }

    public static class ItemCategoryViewModel // get
    {

        public static Expression<Func<ItemCategory, object>> SelectAllItemCategory =>
            x => new
            {
                CategoryName = x.Category
               
            };
        public static Expression<Func<ItemCategory, object>> SelectById =>
            x => new
            {
                CategoryName = x.Category,
                
            };
    }
}
