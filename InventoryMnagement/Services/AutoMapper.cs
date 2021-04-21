using AutoMapper;
using InventoryMnagement.Models;
using InventoryMnagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMnagement.Services
{
    public class AutoMapper :Profile
    {
        public AutoMapper()
        {
            CreateMap<ItemForm, Item>();
            CreateMap<ItemCategoryForm, ItemCategory>();
            CreateMap<TransactionForm, Transaction>();

        }
    }
}
