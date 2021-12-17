using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using FluentValidation;

namespace Financeiro.App.Commands
{
    public class LogarUsuarioCommand : Command<Usuario>
    {
        public LogarUsuarioCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new LogarUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class LogarUsuarioValidator : AbstractValidator<LogarUsuarioCommand>
    {
        public LogarUsuarioValidator()
        {
            RuleFor(c => c.Login).NotNull().NotEmpty().WithMessage("O campo Login não pode estar vazio");
            RuleFor(c => c.Senha).NotNull().NotEmpty().WithMessage("O campo Senha não pode estar vazio");
        }
    }
}
