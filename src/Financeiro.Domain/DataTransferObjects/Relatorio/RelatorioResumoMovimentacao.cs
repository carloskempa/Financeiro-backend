using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Domain.DataTransferObjects.Relatorio
{
    public class RelatorioResumoMovimentacao
    {
        public string MovimentoId { get; set; }
        public string PessoaId { get; set; }
        public string NomePessoa { get; set; }
        public int AnoMes { get; set; }
        public int Ano { get; set; }
        public int MyProperty { get; set; }
    }
}
