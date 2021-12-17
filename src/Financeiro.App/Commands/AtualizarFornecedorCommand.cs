using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;
using System;

namespace Financeiro.App.Commands
{
    public class AtualizarFornecedorCommand: Command<Fornecedor>
    {
        public AtualizarFornecedorCommand(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get;private set; }
        public string Nome { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarFornecedorValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
    public class AtualizarFornecedorValidator : AbstractValidator<AtualizarFornecedorCommand>
    {
        public AtualizarFornecedorValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(Fornecedor.NOME_LENGHT).WithMessage($"O Nome não pode ter mais de {Fornecedor.NOME_LENGHT} caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome não pode estar vazio");
            RuleFor(c => c.Id).NotNull().NotEmpty().WithMessage("O campo Id não pode estar vazio");
        }
    }
}
