using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterPorId(Guid usuarioId);
        Task<Usuario> ObterPorLogin(string login);
        void Cadastrar(Usuario usuario);
        void Atualizar(Usuario usuario);
    }
}
