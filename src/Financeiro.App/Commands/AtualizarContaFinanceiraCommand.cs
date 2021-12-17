using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;
using System;

namespace Financeiro.App.Commands
{
    public class AtualizarContaFinanceiraCommand : Command<ContaFinanceira>
    {
        public AtualizarContaFinanceiraCommand(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarContaFinanceiraValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class AtualizarContaFinanceiraValidator : AbstractValidator<AtualizarContaFinanceiraCommand>
    {
        public AtualizarContaFinanceiraValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(ContaFinanceira.NOME_LENGHT).WithMessage($"O Nome da conta financeira não pode ter mais de {ContaFinanceira.NOME_LENGHT} caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome da conta financeira não pode estar vazio");
            RuleFor(c => c.Id).NotNull().NotEmpty().WithMessage("O campo Id não pode estar vazio");
        }
    }
}
