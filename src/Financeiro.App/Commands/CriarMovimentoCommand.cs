using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Enuns;
using FluentValidation;
using System;

namespace Financeiro.App.Commands
{
    public class CriarMovimentoCommand : Command<Movimento>
    {
        public CriarMovimentoCommand(string descricao, 
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

        public override bool EhValido()
        {
            ValidationResult = new CriarMovimentoValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CriarMovimentoValidator : AbstractValidator<CriarMovimentoCommand>
    {
        public CriarMovimentoValidator()
        {
            RuleFor(c => c.Observacao).MaximumLength(Movimento.OBSERVACAO_LENGHT).WithMessage($"O campo Observação não pode ter mais de {Movimento.OBSERVACAO_LENGHT} caracteres");
            RuleFor(c => c.Descricao).MaximumLength(Movimento.DESCRICAO_LENGHT).WithMessage($"O campo Descrição não pode ter mais de {Movimento.DESCRICAO_LENGHT} caracteres");
            RuleFor(c => c.Descricao).NotNull().NotEmpty().WithMessage("O campo Descrição não pode estar vazio");
            RuleFor(c => c.ValorMovimento).GreaterThan(0).WithMessage("O valor do Movimento não pode ser 0.");
            RuleFor(c => c.PessoaId).NotNull().NotEmpty().WithMessage("O campo Pessoa não pode estar vazio");
            RuleFor(c => c.CentroCustoId).NotNull().NotEmpty().WithMessage("O campo Centro de Custo não pode estar vazio");
            RuleFor(c => c.FornecedorId).NotNull().NotEmpty().WithMessage("O campo Fornecedor não pode estar vazio");
            RuleFor(c => c.ContaId).NotNull().NotEmpty().WithMessage("O campo Conta não pode estar vazio");
        }
    }

}
