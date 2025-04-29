using FinsharkClone.Interfaces;
using FinsharkClone.Modals;
using FinsharkClone.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FinsharkClone.Data;
using System.Threading.Tasks;

namespace FinsharkClone.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }
    }
}