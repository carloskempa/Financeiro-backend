using Financeiro.Domain.Core.Messages;
using FluentValidation;
using System;

namespace Financeiro.App.Commands
{
    public class DeletarCentroCustoCommand : Command<bool>
    {
        public DeletarCentroCustoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DeletarCentroCustoValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DeletarCentroCustoValidator : AbstractValidator<DeletarCentroCustoCommand>
    {
        public DeletarCentroCustoValidator()
        {
            RuleFor(c => c.Id).GreaterThan(Guid.Empty).WithMessage("O campo Id não pode estar vazio");
        }
    }
}
