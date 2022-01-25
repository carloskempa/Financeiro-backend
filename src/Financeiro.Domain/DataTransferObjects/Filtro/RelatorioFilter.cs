using System;

namespace Financeiro.Domain.DataTransferObjects.Filtro
{
    public class RelatorioFilter
    {
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public string ContaId { get; set; }
        public string PessoaId { get; set; }
        public string CentroCustoId { get; set; }
        public int TipoMovimentacao { get; set; }
    }
}
