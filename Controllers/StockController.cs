using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            var stock=await _context.Stocks.ToListAsync();
            var stockDto=stock.Select(s=>s.ToStockDto());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var stock=await _context.Stocks.FindAsync(id);
            if(stock==null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto){
            var stockModel=stockDto.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id=stockModel.Id},stockModel.ToStockDto());
        }

        [HttpPut]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){
            var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null){
                return NotFound();
            }
            stockModel.CompanyName=updateDto.CompanyName;
            stockModel.Symbol=updateDto.Symbol;
            stockModel.Purchase=updateDto.Purchase;
            stockModel.LastDiv=updateDto.LastDiv;
            stockModel.MarketCap=updateDto.MarketCap;

            await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());

        }

        [HttpDelete("{id}")]
        // [FromRoute("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null){
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }
    }
}