using FinsharkClone.Modals;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinsharkClone.Dtos.Stock;
using FinsharkClone.Helpers;

namespace FinsharkClone.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
