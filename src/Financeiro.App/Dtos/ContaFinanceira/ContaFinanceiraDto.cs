using System;

namespace Financeiro.App.Dtos
{
    public class ContaFinanceiraDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
