using System;

namespace Financeiro.App.Dtos.Pessoa
{
    public class PessoaCentroCustoDto
    {
        public Guid PessoaId { get; set; }
        public Guid CentroCustoId { get; set; }
        public decimal ValorMensal { get; set; }
    }
}
