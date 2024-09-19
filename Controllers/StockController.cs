using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using myFirstAPI.Data;
using myFirstAPI.DTOs.Stock;
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto){
            var stockModel=stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),new {id=stockModel.Id},stockModel.ToStockDto());
        }

        [HttpPut]
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){
            var stockModel=_context.Stocks.FirstOrDefault(x=>x.Id==id);
            if(stockModel==null){
                return NotFound();
            }
            stockModel.CompanyName=updateDto.CompanyName;
            stockModel.Symbol=updateDto.Symbol;
            stockModel.Purchase=updateDto.Purchase;
            stockModel.LastDiv=updateDto.LastDiv;
            stockModel.MarketCap=updateDto.MarketCap;

            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());

        }

        [HttpDelete("{id}")]
        // [FromRoute("{id}")]
        public IActionResult Delete([FromRoute] int id){
            var stockModel=_context.Stocks.FirstOrDefault(x=>x.Id==id);
            if(stockModel==null){
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }
    }
}