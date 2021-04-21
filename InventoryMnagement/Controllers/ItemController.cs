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
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ItemController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        // GET: api/<ItemController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _appDbContext.Items
                 .Select(ItemViewModel.SelectAllItem)
                 .ToListAsync();

            return Ok(res);

        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _appDbContext.Items
                .Where(x => x.Id == id && x.ItemCategory != null)
                .Include(x => x.ItemCategory)
                .Select(ItemViewModel.SelectById)
                .FirstOrDefaultAsync();

            return Ok(res);
        }

        // POST api/<ItemController>
        [HttpPost]

        public async Task<IActionResult> PostAsync([FromBody] ItemForm itemForm)
        {
            try
            {
                var modelresources = _mapper.Map<Item>(itemForm);
                await _appDbContext.Items.AddAsync(modelresources);
                await _appDbContext.SaveChangesAsync();

                var res = await _appDbContext.Items
                    .Select(ItemViewModel.SelectAllItem)
                    .ToListAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        // PUT api/<ItemController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ItemForm itemForm)
        {

            try
            {
                var modelresources = _mapper.Map<Item>(itemForm);
                _appDbContext.Items.Update(modelresources);
                _appDbContext.SaveChanges();

                var res = await _appDbContext.Items
                     .Select(ItemViewModel.SelectAllItem)
                     .ToListAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                _appDbContext.Items.Remove(new Item { Id = id });
                await _appDbContext.SaveChangesAsync();

                var res = await _appDbContext.Items
                    .Select(ItemViewModel.SelectAllItem)
                    .ToListAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPut("AditionToQuantity")]
        public async Task<IActionResult> AddQuantity([FromBody] ItemForm itemFrom)
        {
            try
            {

                Item item = new Item()
                {
                    Id = itemFrom.Id,
                    Quantity = itemFrom.Quantity
                };



                var executionStrategy = _appDbContext.Database.CreateExecutionStrategy();
                await executionStrategy.ExecuteAsync(async () =>
                {
                    using var transaction = await _appDbContext.Database.BeginTransactionAsync();
                    try
                    {


                        Item currentItem = await _appDbContext.Items.Where(x => x.Id == itemFrom.Id).FirstOrDefaultAsync();
                      
                        currentItem.Quantity += itemFrom.Quantity;

                        _appDbContext.Items.Update(currentItem);
                        await _appDbContext.SaveChangesAsync();

                        await _appDbContext.Transactions.AddAsync(new Transaction
                        {
                            ItemId = currentItem.Id,
                            Quantity = itemFrom.Quantity,
                            Status = 1,
                            CreatedTime = DateTime.Now,
                            CreatedUser = "Saman"
                        });
                        await _appDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        
                        var res = await _appDbContext.Items.Include(x => x.ItemCategory).ToListAsync();
                        return Ok(res);
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


            var res = await _appDbContext.Transactions.Include(x => x.Item).ToListAsync();
            return Ok(res);


        }
        [HttpPut("Substract")]
        public async Task<IActionResult> SubstractQuantity([FromBody] ItemForm itemForm)
        {
            try
            {
                Item item = new Item()
                {
                    Id = itemForm.Id,
                    Quantity = itemForm.Quantity
                };
                var executionStrategy = _appDbContext.Database.CreateExecutionStrategy();



          



                await executionStrategy.ExecuteAsync(async () =>
                {
                    using var transaction = await _appDbContext.Database.BeginTransactionAsync();
                    try
                    {
                        var data = await _appDbContext.Items.Where(x => x.Id == itemForm.Id).FirstOrDefaultAsync();

                        if (data.ThresholdEnable == true && data.Quantity > itemForm.Quantity)
                        {
                            data.Quantity -= itemForm.Quantity;
                            _appDbContext.Items.Update(data);
                            await _appDbContext.SaveChangesAsync();
                        }

                        await _appDbContext.Transactions.AddAsync(new Transaction
                        {
                            ItemId = data.Id,
                            Quantity = data.Quantity,
                            Status = 0,
                            CreatedTime = DateTime.Now,
                            CreatedUser = "Saman"
                        });
                        await _appDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();


                        var res = await _appDbContext.Items.Include(x => x.ItemCategory).ToListAsync();
                        return Ok(res);
                    }
                    catch (Exception)
                    {

                        await transaction.RollbackAsync();
                        throw;
                    }
                });

            }




            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            var res = await _appDbContext.Transactions.Include(x => x.Item).ToListAsync();
            return Ok(res);

        }
    }
}
