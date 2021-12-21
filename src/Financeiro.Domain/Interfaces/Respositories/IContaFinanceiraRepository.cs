using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IContaFinanceiraRepository : IRepository<ContaFinanceira>
    {
        Task<ContaFinanceira> ObterPorId(Guid id);
        void Cadastrar(ContaFinanceira entity);
        void Atualizar(ContaFinanceira entity);
        void Deletar(ContaFinanceira entity);
    }
}
