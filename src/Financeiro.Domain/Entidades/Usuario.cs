using Financeiro.Domain.Core.DomainObjects;
using System;

namespace Financeiro.Domain.Entidades
{
    public class Usuario : Entity
    {
        public Usuario(string nome, string login, string senha, bool ativo)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Ativo = ativo;

            Validar();
        }
        protected Usuario() { }

        public const int NOME_LENGHT = 100;
        public const int LOGIN_LENGHT = 50;

        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public bool Ativo { get; private set; }
        public string RefreshToken { get; private set; }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
        public void CriarRefreshToken() => RefreshToken = Guid.NewGuid().ToString().Replace("-", "");

        public void AlterarSenha(string senha)
        {
            Validacoes.ValidarSeNulo(Senha, "O campo Senha não pode estar vazio");
            this.Senha = senha;
        }

        public void Atualizar(string nome, string login, bool ativo)
        {
            Nome = nome;
            Login = login;
            Ativo = ativo;

            Validar();
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome não pode estar vazio.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"O campo Nome não pode ter mais de {NOME_LENGHT} caracteres.");
            Validacoes.ValidarSeVazio(Login, "O campo Login não pode estar vazio.");
            Validacoes.ValidarTamanho(Login, LOGIN_LENGHT, $"O campo Login não pode ter mais de {LOGIN_LENGHT} caracteres.");
            Validacoes.ValidarSeNulo(Senha, "O campo Senha não pode estar vazio.");
        }
    }
}
