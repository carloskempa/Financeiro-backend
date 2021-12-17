using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.App.Dtos.ContaFinanceira
{
    public class DepositoDto
    {
        public Guid ContaFinanceiraId { get; set; }
        public decimal ValorDeposito { get; set; }
    }
}
