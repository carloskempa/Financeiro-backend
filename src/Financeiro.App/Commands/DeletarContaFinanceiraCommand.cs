using Financeiro.Domain.Core.Messages;
using FluentValidation;
using System;

namespace Financeiro.App.Commands
{
    public class DeletarContaFinanceiraCommand : Command<bool>
    {
        public DeletarContaFinanceiraCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DeletarContaFinanceiraValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class DeletarContaFinanceiraValidator : AbstractValidator<DeletarContaFinanceiraCommand>
    {
        public DeletarContaFinanceiraValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().GreaterThan(Guid.Empty).WithMessage("O Campo Id não pode estar vazio.");
        }
    }
}
