using System.Threading.Tasks;

namespace Bitcoin.Core
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}