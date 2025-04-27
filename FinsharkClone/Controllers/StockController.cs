
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

        
    }
}