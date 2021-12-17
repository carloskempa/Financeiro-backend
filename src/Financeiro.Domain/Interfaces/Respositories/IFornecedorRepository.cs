using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterPorId(Guid id);
        Task<Fornecedor> ObterPorNome(string nome);
        void Cadastrar(Fornecedor entity);
        void Atualizar(Fornecedor entity);
    }
}
