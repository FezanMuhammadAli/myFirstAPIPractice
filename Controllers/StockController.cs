using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using myFirstAPI.Data;
using myFirstAPI.Mappers;

namespace myFirstAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context=context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stock= _context.Stocks.ToList().Select(s=>s.ToStockDto());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var stock= _context.Stocks.Find(id);
            if(stock==null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
    }
}