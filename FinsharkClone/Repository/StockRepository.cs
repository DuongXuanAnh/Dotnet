using FinsharkClone.Interfaces;
using FinsharkClone.Modals;
using FinsharkClone.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FinsharkClone.Data;
using System.Threading.Tasks;
using FinsharkClone.Helpers;

namespace FinsharkClone.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.CompanyName)){
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol)){
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            stocks = stocks.Skip(skipNumber).Take(queryObject.PageSize);

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id){
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol){
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<bool> StockExists(int id){
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto){
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(existingStock == null){
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
            
        }

        public async Task<Stock> CreateAsync(Stock stockModel){
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id){
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel == null){
                return null;
            }
            
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}