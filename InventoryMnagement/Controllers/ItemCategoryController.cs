using AutoMapper;
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
        private readonly IMapper _mapper;

        public ItemCategoryController(
            AppDbContext appDbContext, 
            IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        // GET: api/<ItemCategoryController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var res = await _appDbContext.ItemCategories.
                       Select(ItemCategoryViewModel.SelectAllItemCategory).
                       ToListAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        // GET api/<ItemCategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var res = await _appDbContext.ItemCategories.
                    Where(x => x.Id == id).
                    Select(ItemCategoryViewModel.SelectById).
                    FirstOrDefaultAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
                
        }

        // POST api/<ItemCategoryController>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ItemCategoryForm itemCategoryForm)
        {
            try
            {
                var modelResources = _mapper.Map<ItemCategory>(itemCategoryForm);
                await _appDbContext.ItemCategories.AddAsync(modelResources);
                await _appDbContext.SaveChangesAsync();

                var res = await _appDbContext.ItemCategories.
                  Select(ItemCategoryViewModel.SelectAllItemCategory).
                  ToListAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        // PUT api/<ItemCategoryController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ItemCategoryForm itemCategoryForm)
        {
            ItemCategory itemCategory = new ItemCategory()
            {
                Id = itemCategoryForm.Id,
                Category = itemCategoryForm.Category

            };
            await _appDbContext.ItemCategories.AddAsync(itemCategory);
            await _appDbContext.SaveChangesAsync();

            var res = await _appDbContext.ItemCategories.
              Select(ItemCategoryViewModel.SelectAllItemCategory).
              ToListAsync();

            return Ok(res);
        }

        // DELETE api/<ItemCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _appDbContext.ItemCategories.Remove(new ItemCategory { Id = id });
        }
    }
}
