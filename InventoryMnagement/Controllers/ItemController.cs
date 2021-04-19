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
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ItemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/<ItemController>
        [HttpGet]
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
           return  await _appDbContext.Items.Include(x => x.ItemCategory).ToListAsync();
           
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public async Task<Item> Get(int id)
        {
            return await _appDbContext.Items.Include(x => x.ItemCategory).FirstOrDefaultAsync(x => x.Id == id);
        }

        // POST api/<ItemController>
        [HttpPost]
        public async Task<IEnumerable<Item>> PostAsync([FromBody] ItemViewModel itemViewModel)
        {
            Item item = new Item()
            {
                
                ItemName = itemViewModel.ItemName,
                Quantity = itemViewModel.Quantity,
                ThresholdEnable = itemViewModel.ThresholdEnable,
                MinThreshold = itemViewModel.MinThreshold,
                ItemCategoryId = itemViewModel.ItemCategoryId
            };

            await _appDbContext.Items.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return await _appDbContext.Items.Include(x => x.ItemCategory).ToListAsync();
        }

        // PUT api/<ItemController>/5
        [HttpPut]
        public async Task<IEnumerable<Item>> UpdateAsync([FromBody] ItemViewModel itemViewModel)
        {
            Item item = new Item()
            {
                Id = itemViewModel.Id,
                ItemName = itemViewModel.ItemName,
                Quantity = itemViewModel.Quantity,
                ThresholdEnable = itemViewModel.ThresholdEnable,
                MinThreshold = itemViewModel.MinThreshold,
                ItemCategoryId = itemViewModel.ItemCategoryId
            };

            _appDbContext.Items.Update(item);
            _appDbContext.SaveChanges();

            return await _appDbContext.Items.Include(x => x.ItemCategory).ToListAsync();
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public async Task<IEnumerable<Item>> DeleteAsync(int id)
        {
           _appDbContext.Items.Remove(new Item { Id = id });
           await _appDbContext.SaveChangesAsync();
            return await _appDbContext.Items.Include(x => x.ItemCategory).ToListAsync();
        }
    }
}
