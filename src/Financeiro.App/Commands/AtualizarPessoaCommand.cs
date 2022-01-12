using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Financeiro.App.Commands
{
    public class AtualizarPessoaCommand : Command<Pessoa>
    {
        public AtualizarPessoaCommand(Guid id, string nome, List<PessoaCentroCusto> pessoaCentroCustos)
        {
            Id = id;
            Nome = nome;
            PessoaCentroCustos = pessoaCentroCustos;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public List<PessoaCentroCusto> PessoaCentroCustos { get; private set; }


        public override bool EhValido()
        {
            ValidationResult = new AtualizarPessoaValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarPessoaValidator : AbstractValidator<AtualizarPessoaCommand>
    {
        public AtualizarPessoaValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(Guid.Empty).WithMessage("O campo Id nao pode estar vazio.");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo nome nao pode estar vazio.");
            RuleForEach(c => c.PessoaCentroCustos).Must(p => p.PessoaId.Equals(Guid.Empty)).WithMessage("O Id da Pessoa do Orçamento não pode estar vazio.");
            RuleForEach(c => c.PessoaCentroCustos).Must(p => p.CentroCustoId.Equals(Guid.Empty)).WithMessage("O Id do centro de custo não pode estar vazio.");
            RuleForEach(c => c.PessoaCentroCustos).Must(p => p.ValorMensal < 0).NotNull().WithMessage("O Campo valor mensal não pode ser menor que zero.");
        }
    }
}
