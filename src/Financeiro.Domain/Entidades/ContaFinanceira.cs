using Financeiro.Domain.Core.DomainObjects;
using System.Collections.Generic;

namespace Financeiro.Domain.Entidades
{
    public class ContaFinanceira : Entity
    {
        public ContaFinanceira(string nome)
        {
            Nome = nome;
            Validar();
        }
        protected ContaFinanceira() { }

        public const int NOME_LENGHT = 100;

        public string Nome { get; private set; }
        public ICollection<Movimento> Movimentos { get; private set; }

        public void Atualizar(string nome)
        {
            Nome = nome;
            Validar();
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O nome da conta não pode ser vazio.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"O nome da conta não pode ter mais de {NOME_LENGHT} caracteres.");
        }
    }
}
