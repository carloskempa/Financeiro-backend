using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;

namespace Financeiro.App.Commands
{
    public class CriarCentroCustoCommand : Command<CentroCusto>
    {
        public CriarCentroCustoCommand(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CriarCentroCustoValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
    public class CriarCentroCustoValidator : AbstractValidator<CriarCentroCustoCommand>
    {
        public CriarCentroCustoValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(CentroCusto.NOME_LENGHT).WithMessage($"O Nome não pode ter mais de {CentroCusto.NOME_LENGHT} caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome não pode estar vazio");
        }
    }
}
