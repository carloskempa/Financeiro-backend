using System;
using System.Collections.Generic;

namespace Financeiro.App.Dtos.Pessoa
{
    public class PessoaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<PessoaCentroCustoDto> PessoaCentroCustos { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtAtualizado { get; set; }
    }
}
