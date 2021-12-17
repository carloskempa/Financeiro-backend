using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;

namespace Financeiro.App.Commands
{
    public class CriarContaFinanceiraCommand : Command<ContaFinanceira>
    {
        public CriarContaFinanceiraCommand(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CriarContaFinanceiraValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class CriarContaFinanceiraValidator : AbstractValidator<CriarContaFinanceiraCommand>
    {
        public CriarContaFinanceiraValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(ContaFinanceira.NOME_LENGHT).WithMessage($"O Nome da conta financeira não pode ter mais de {ContaFinanceira.NOME_LENGHT} caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome da conta financeira não pode estar vazio");
        }
    }
}
