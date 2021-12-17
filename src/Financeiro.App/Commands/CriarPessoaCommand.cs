using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;

namespace Financeiro.App.Commands
{
    public class CriarPessoaCommand : Command<Pessoa>
    {
        public CriarPessoaCommand(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CriarPessoaValidador().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CriarPessoaValidador : AbstractValidator<CriarPessoaCommand>
    {
        public CriarPessoaValidador()
        {
            RuleFor(c => c.Nome).MaximumLength(Pessoa.NOME_LENGHT).WithMessage($"Nome não pode ter mais de {Pessoa.NOME_LENGHT} caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome não pode estar vazio");
        }
    }

}
