using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IItemMovimentoRepository : IRepository<ItemMovimento>
    {
        Task<IEnumerable<ItemMovimento>> ObterItensMovimento(Guid movimentoId);
        Task<ItemMovimento> ObterPorId(Guid id);
        Task<IEnumerable<ItemMovimento>> Buscar(Expression<Func<ItemMovimento, bool>> predicado);
        void Cadastrar(ItemMovimento entity);
        void Atualizar(ItemMovimento entity);
        void Deletar(ItemMovimento entity);
    }
}
