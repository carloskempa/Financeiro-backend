using System;

namespace Financeiro.App.Dtos.CentroCusto
{
    public class CentroCustoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
