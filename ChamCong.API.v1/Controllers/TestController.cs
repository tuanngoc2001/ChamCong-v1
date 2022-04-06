using ChamCong.API.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamCong.API.v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ImDbContext _dbcontext;
        public TestController(ImDbContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public IActionResult getall()
        {
            return Ok(_dbcontext.im_User_Group.ToList());
        }
    }
}
