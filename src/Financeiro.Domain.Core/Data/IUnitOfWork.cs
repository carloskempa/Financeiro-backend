using System.Threading.Tasks;

namespace Financeiro.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
