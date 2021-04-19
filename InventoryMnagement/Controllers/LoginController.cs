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
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public LoginController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

       [HttpGet]
       public async Task<IActionResult> CheckLoginDetailsAsync([FromQuery]LoginInfoViewModel loginInfoViewModel)
        {
           var data = await _appDbContext.LoginInfos.Where(x => x.UserName == loginInfoViewModel.UserName).FirstOrDefaultAsync();

            if(data.Password == loginInfoViewModel.Password)
            {
                return Ok("valid");
            }

            return Unauthorized();
        }
    }
}
