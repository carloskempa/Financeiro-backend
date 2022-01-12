using Financeiro.Domain.Core.Messages;
using FluentValidation;
using System;

namespace Financeiro.App.Commands
{
    public class DeletarPessoaCommand: Command<bool>
    {
        public DeletarPessoaCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get;private set; }
        public override bool EhValido()
        {
            ValidationResult = new DeletarPessoaValidators().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DeletarPessoaValidators : AbstractValidator<DeletarPessoaCommand>
    {
        public DeletarPessoaValidators()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().GreaterThan(Guid.Empty).WithMessage("O Campo Id não pode estar vazio.");
        }
    }
}
