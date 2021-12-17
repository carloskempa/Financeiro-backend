using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;

namespace Financeiro.App.Commands
{
    public class CriarUsuarioCommand : Command<Usuario>
    {
        public CriarUsuarioCommand(string nome, string login, string senha, bool ativo)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public string Nome { get;private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class AdicionarUsuarioValidator : AbstractValidator<CriarUsuarioCommand>
    {
        public AdicionarUsuarioValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(Usuario.NOME_LENGHT).WithMessage($"O Nome do usuário não pode ter mais de {Usuario.NOME_LENGHT} caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome não pode estar vazio");
            RuleFor(c => c.Login).MaximumLength(Usuario.LOGIN_LENGHT).WithMessage($"O campo Login não pode ter mais de {Usuario.LOGIN_LENGHT} caracteres");
            RuleFor(c => c.Login).NotNull().NotEmpty().WithMessage("O campo Login não pode estar vazio");
            RuleFor(c => c.Senha).MaximumLength(20).WithMessage("O campo Senha não pode ter mais de 20 caracteres");
            RuleFor(c => c.Senha).MinimumLength(6).WithMessage("O campo Senha tem que ter no minímo 6 caracteres");
            RuleFor(c => c.Senha).NotNull().NotEmpty().WithMessage("O campo Senha não pode estar vazio");
        }
    }

}
