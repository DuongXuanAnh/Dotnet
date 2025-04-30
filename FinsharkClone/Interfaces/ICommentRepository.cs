using FinsharkClone.Modals;
using System.Threading.Tasks;

namespace FinsharkClone.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}