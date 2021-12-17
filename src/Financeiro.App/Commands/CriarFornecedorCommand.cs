using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;

namespace Financeiro.App.Commands
{
    public class CriarFornecedorCommand : Command<Fornecedor>
    {
        public CriarFornecedorCommand(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CriarFornecedorValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
    public class CriarFornecedorValidator : AbstractValidator<CriarFornecedorCommand>
    {
        public CriarFornecedorValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(Fornecedor.NOME_LENGHT).WithMessage($"O Nome não pode ter mais de {Fornecedor.NOME_LENGHT} caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome não pode estar vazio");
        }
    }
}
