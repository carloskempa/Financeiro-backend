using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IMovimentoRepository : IRepository<Movimento>
    {
        Task<Movimento> ObterPorId(Guid id);
        void Cadastrar(Movimento entity);
        void Atualizar(Movimento entity);
        void Deletar(Movimento entity);
    }
}
