using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface ICentroCustoRepository : IRepository<CentroCusto>
    {
        Task<CentroCusto> ObterPorId(Guid id);
        Task<CentroCusto> ObterPeloNome(string nome);
        void Cadastrar(CentroCusto entity);
        void Atualizar(CentroCusto entity);
    }
}
