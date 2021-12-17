using Financeiro.Domain.Core.DomainObjects;
using Financeiro.Domain.Enuns;
using System;
using System.Collections.Generic;

namespace Financeiro.Domain.Entidades
{
    public class Movimento : Entity
    {

        public const int DESCRICAO_LENGHT = 500;
        public const int OBSERVACAO_LENGHT = 1000;

        public Movimento(string descricao,
                         decimal valorMovimento,
                         string observacao,
                         bool isPago,
                         DateTime? dataVencimento,
                         DateTime dataMovimento,
                         TipoMovimento tipoMovimento,
                         Guid contaId,
                         Guid centroCustoId,
                         Guid fornecedorId,
                         Guid pessoaId,
                         Guid? pessoaPagadorId)
        {
            Descricao = descricao;
            ValorMovimento = valorMovimento;
            Observacao = observacao;
            IsPago = isPago;
            DataVencimento = dataVencimento;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            ContaId = contaId;
            CentroCustoId = centroCustoId;
            FornecedorId = fornecedorId;
            PessoaId = pessoaId;
            PessoaPagadorId = pessoaPagadorId;

            Validar();
        }

        public string Descricao { get; private set; }
        public decimal ValorMovimento { get; private set; }
        public string Observacao { get; private set; }
        public bool IsPago { get; private set; }
        public DateTime? DataVencimento { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public TipoMovimento TipoMovimento { get; private set; }
        public Guid ContaId { get; private set; }
        public Guid CentroCustoId { get; private set; }
        public Guid FornecedorId { get; private set; }
        public Guid PessoaId { get; private set; }
        public Guid? PessoaPagadorId { get; private set; }
        public ContaFinanceira Conta { get; private set; }
        public CentroCusto CentroCusto { get; private set; }
        public Fornecedor Fornecedor { get; private set; }
        public Pessoa Pessoa { get; private set; }
        public Pessoa PessoaPagador { get; private set; }


        public ICollection<ItemMovimento> ItensMovimentos { get; private set; }

        public override void Validar()
        {
            Validacoes.ValidarSeIgual(ContaId, Guid.Empty, "A Conta não pode ser vazio.");
            Validacoes.ValidarSeIgual(CentroCustoId, Guid.Empty, "O Centro de custo não pode ser vazio.");
            Validacoes.ValidarSeIgual(FornecedorId, Guid.Empty, "O Fornecedor não pode ser vazio.");
            Validacoes.ValidarSeIgual(PessoaId, Guid.Empty, "A Pessoa não pode ser vazio.");
            Validacoes.ValidarSeNulo(DataMovimento, "A data de movimento não pode ser vazio.");
            Validacoes.ValidarSeIgual(ValorMovimento, 0, "O campo valor do movimento não pode ser 0.");
            Validacoes.ValidarSeVazio(Descricao, "A descrição não pode ser vazio.");
            Validacoes.ValidarTamanho(Descricao, DESCRICAO_LENGHT, $"A descrição não pode ter mais de {DESCRICAO_LENGHT} caracteres.");
            Validacoes.ValidarTamanho(Observacao, OBSERVACAO_LENGHT, $"O campo observação não pode ter mais de {OBSERVACAO_LENGHT} caracteres.");
        }
    }
}
