using Financeiro.App.Dtos.CentroCusto;
using Financeiro.App.Dtos.Pessoa;
using Financeiro.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.App.Dtos
{
    public class MovimentoDto
    {
        public Guid? Id { get; set; }
        public string Descricao { get; set; }
        public decimal ValorMovimento { get; set; }
        public string Observacao { get; set; }
        public bool IsPago { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime DataMovimento { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public Guid ContaId { get; set; }
        public Guid CentroCustoId { get; set; }
        public Guid FornecedorId { get; set; }
        public Guid PessoaId { get; set; }
        public Guid? PessoaPagadorId { get; set; }
        public ContaFinanceiraDto Conta { get; set; }
        public CentroCustoDto CentroCusto { get; set; }
        public FornecedorDto Fornecedor { get; set; }
        public PessoaDto Pessoa { get; set; }
        public PessoaDto PessoaPagador { get; set; }
    }
}
