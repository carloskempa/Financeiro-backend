using System;

namespace Financeiro.Domain.DataTransferObjects.Filtro
{
    public class MovimentoFilter
    {
        public Guid FornecedorId { get; set; }
        public Guid ContaFinanceiraId { get; set; }
        public Guid CentroCustoId { get; set; }
        public Guid PessoaId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }
}
