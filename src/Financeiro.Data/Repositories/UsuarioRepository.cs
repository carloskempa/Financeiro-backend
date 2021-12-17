using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Financeiro.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FinanceiroContext _context;

        public UsuarioRepository(FinanceiroContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Usuario> ObterPorId(Guid usuarioId)
        {
            return await _context.Usuarios.FindAsync(usuarioId);
        }

        public async Task<Usuario> ObterPorLogin(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c=>c.Login == login);
        }

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
