using FinsharkClone.Modals;

namespace FinsharkClone.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
    }
}