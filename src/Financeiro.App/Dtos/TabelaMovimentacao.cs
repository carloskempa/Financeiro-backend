using Canducci.Pagination;
using Financeiro.Domain.Entidades;
using System.Linq;

namespace Financeiro.App.Dtos
{
    public class TabelaMovimentacao
    {
        public PaginatedRest<Movimento> Data { get; set; }
        public decimal TotalEntrada
        {
            get
            {
                if (!Data.Items.Any())
                    return 0;

                return Data.Items.Where(c => c.TipoMovimento == Domain.Enuns.TipoMovimento.Entrada)
                                 .Select(c => c.ValorMovimento)
                                 .Sum();
            }
        }

        public decimal TotalSaida
        {
            get
            {
                if (!Data.Items.Any())
                    return 0;

                return Data.Items.Where(c => c.TipoMovimento == Domain.Enuns.TipoMovimento.Saida)
                                 .Select(c => c.ValorMovimento)
                                 .Sum();
            }
        }

        public decimal ReceiraSaida
        {
            get
            {
                return TotalEntrada - TotalSaida;
            }
        }

    }
}
