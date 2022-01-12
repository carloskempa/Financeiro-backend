using Canducci.Pagination;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Data.Queries
{
    public class MovimentoQuery : IMovimentoQuery
    {
        private readonly FinanceiroContext _context;

        public MovimentoQuery(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task<PaginatedRest<Movimento>> MovimentoFilter(MovimentoFilter filter, Paginacao paginacao)
        {
            var query = from mov in _context.Movimentos
                                            .Include(c => c.Pessoa)
                                            .Include(c => c.CentroCusto)
                                            .Include(c => c.Fornecedor)
                                            .Include(c => c.Conta)
                                            .Include(c => c.ItensMovimentos)
                                            .AsNoTracking()
                        where(mov.DataMovimento.Month == filter.Mes && 
                              mov.DataMovimento.Year == filter.Ano)
                        select mov;

            if (filter.CentroCustoId != Guid.Empty)
                query = query.Where(c => c.CentroCustoId == filter.CentroCustoId);

            if (filter.ContaFinanceiraId != Guid.Empty)
                query = query.Where(c => c.ContaId == filter.ContaFinanceiraId);

            if (filter.FornecedorId != Guid.Empty)
                query = query.Where(c => c.FornecedorId == filter.FornecedorId);

            if (filter.PessoaId != Guid.Empty)
                query = query.Where(c => c.PessoaId == filter.PessoaId);


            return await query.OrderByDescending(c => c.DtCadastro)
                              .ToPaginatedRestAsync(paginacao.Page == 0 ? 1 : paginacao.Page,
                                                    paginacao.PageSize == 0 ? 10 : paginacao.PageSize);
        }
    }
}
