using InventoryMnagement.Data;
using InventoryMnagement.Models;
using InventoryMnagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMnagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ItemCategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/<ItemCategoryController>
        [HttpGet]
        public async Task<IEnumerable<ItemCategory>> GetAllAsync()
        {
            return await _appDbContext.ItemCategories.ToListAsync();
        }

        // GET api/<ItemCategoryController>/5
        [HttpGet("{id}")]
        public async Task<ItemCategory> GetAsync(int id)
        {
            return await _appDbContext.ItemCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        // POST api/<ItemCategoryController>
        [HttpPost]
        public async Task<IEnumerable<ItemCategory>> CreateAsync([FromBody] ItemCategoryViewModel itemCategoryViewModel)
        {
            ItemCategory itemCategory = new ItemCategory()
            {
                Category = itemCategoryViewModel.Category
            
            };
            await _appDbContext.ItemCategories.AddAsync(itemCategory);
            await _appDbContext.SaveChangesAsync();

            return await _appDbContext.ItemCategories.ToListAsync();
        }

        // PUT api/<ItemCategoryController>/5
        [HttpPut]
        public async Task<IEnumerable<ItemCategory>> Put([FromBody] ItemCategoryViewModel itemCategoryViewModel)
        {
            ItemCategory itemCategory = new ItemCategory()
            {
                Id = itemCategoryViewModel.Id,
                Category = itemCategoryViewModel.Category

            };
            await _appDbContext.ItemCategories.AddAsync(itemCategory);
            await _appDbContext.SaveChangesAsync();

            return await _appDbContext.ItemCategories.ToListAsync();
        }

        // DELETE api/<ItemCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _appDbContext.ItemCategories.Remove(new ItemCategory { Id = id });
        }
    }
}
