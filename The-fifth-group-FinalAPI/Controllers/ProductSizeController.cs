using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizeController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ProductSizeController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        [HttpGet("sizes")]
        public async Task<ActionResult<IEnumerable<int>>> GetProductSizes()
        {
            var sizes = await _context.ProductSize.Select(s => s.Size).ToListAsync();
            return Ok(sizes);
        }

    }

}
