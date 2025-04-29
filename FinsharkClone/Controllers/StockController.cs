
using FinsharkClone.Data;
using FinsharkClone.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinsharkClone.Dtos.Stock;
using FinsharkClone.Mappers;

namespace FinsharkClone.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
        
            var stocks = await _context.Stocks.ToListAsync();
            var stockDtos = stocks.Select(s => s.ToStockDto());


            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
           var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

           if (stockModel == null)
           {
            return NotFound();
           }

           stockModel.Symbol = stockDto.Symbol;
           stockModel.CompanyName = stockDto.CompanyName;
           stockModel.Purchase = stockDto.Purchase;
           stockModel.LastDiv = stockDto.LastDiv;
           stockModel.Industry = stockDto.Industry;
           stockModel.MarketCap = stockDto.MarketCap;

           await _context.SaveChangesAsync();

           return Ok(stockModel.ToStockDto());
            
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}