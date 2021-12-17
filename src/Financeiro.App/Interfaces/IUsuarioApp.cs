using Financeiro.App.Dtos;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IUsuarioApp
    {
        Task<RetornoPadrao<UsuarioDto>> ObterPorId(Guid id);
        Task<RetornoPadrao<UsuarioDto>> Logar(UsuarioLogin usuario);
        Task<RetornoPadrao<UsuarioDto>> Cadastrar(UsuarioDto usuario);
    }
}
