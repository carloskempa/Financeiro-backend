using System;

namespace Financeiro.App.Dtos
{
    public class FornecedorDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtAtualizacao { get; set; }
    }
}
