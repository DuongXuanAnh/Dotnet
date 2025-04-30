using FinsharkClone.Modals;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinsharkClone.Dtos.Stock;

namespace FinsharkClone.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
    }
}