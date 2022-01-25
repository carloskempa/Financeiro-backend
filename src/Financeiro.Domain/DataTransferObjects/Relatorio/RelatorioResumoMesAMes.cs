using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Domain.DataTransferObjects.Relatorio
{
   public class RelatorioResumoMesAMes
    {
        public string MovimentoId { get; set; }
        public string PessoaId { get; set; }
        public string NomePessoa { get; set; }
        public string AnoMes { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string NomeMes { get; set; }
        public string NomeCentroCusto { get; set; }
        public string NomeFornecedor { get; set; }
        public string DescricaoMovimento { get; set; }
        public decimal ValorMovimento { get; set; }
        public decimal ValorMensal { get; set; }
    }
}
