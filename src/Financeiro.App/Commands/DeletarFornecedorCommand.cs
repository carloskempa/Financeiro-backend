using Financeiro.Domain.Core.Messages;
using FluentValidation;
using System;

namespace Financeiro.App.Commands
{
    public class DeletarFornecedorCommand : Command<bool>
    {
        public DeletarFornecedorCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DeletarFornecedorValidators().Validate(this);

            return ValidationResult.IsValid;
        }
    }
    public class DeletarFornecedorValidators : AbstractValidator<DeletarFornecedorCommand>
    {
        public DeletarFornecedorValidators()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().GreaterThan(Guid.Empty).WithMessage("O Campo Id não pode estar vazio.");
        }
    }
}
