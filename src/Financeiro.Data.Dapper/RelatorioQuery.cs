using Dapper;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.DataTransferObjects.Relatorio;
using Financeiro.Domain.Interfaces.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Data.Dapper
{
    public class RelatorioQuery : IRelatorioQuery
    {
        private readonly DapperContext _context;

        public RelatorioQuery(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RelatorioResumoMesAMes>> ObterResumoMesAMes(RelatorioFilter filter)
        {
            var connection = _context.ObterConexao();
            var consulta = await connection.QueryAsync<RelatorioResumoMesAMes>("rpt_RESUMO_MOVIMENTACAO_MES_MES", ObterParams(filter), commandType: System.Data.CommandType.StoredProcedure);
            return consulta;
        }

        public Task<IEnumerable<RelatorioResumoMovimentacao>> ObterResumoMovimentacao(RelatorioFilter filter)
        {
            throw new NotImplementedException();
        }

        private static DynamicParameters ObterParams(RelatorioFilter filter)
        {
            var parametros = new DynamicParameters();

            parametros.Add("@DataInicial", filter.DtInicial);
            parametros.Add("@DataFinal", filter.DtFinal);
            parametros.Add("@TipoMovimento", filter.TipoMovimentacao);
            parametros.Add("@ContaId", filter.ContaId);
            parametros.Add("@PessoaId", filter.PessoaId);
            parametros.Add("@CentroCustoId", filter.CentroCustoId);

            return parametros;
        }
    }
}
