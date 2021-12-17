using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IItemMovimentoRepository : IRepository<ItemMovimento>
    {
        Task<IEnumerable<ItemMovimento>> ObterItensMovimento(Guid movimentoId);
        Task<ItemMovimento> ObterPorId(Guid id);
        void Cadastrar(ItemMovimento entity);
        void Atualizar(ItemMovimento entity);
        void Deletar(ItemMovimento entity);
    }
}
