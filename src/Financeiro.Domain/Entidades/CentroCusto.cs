using Financeiro.Domain.Core.DomainObjects;
using System.Collections.Generic;

namespace Financeiro.Domain.Entidades
{
    public class CentroCusto : Entity
    {
        public CentroCusto(string nome)
        {
            Nome = nome;
            Validar();
        }

        public const int NOME_LENGHT = 100;

        protected CentroCusto() { }
        public string Nome { get; private set; }
        public ICollection<PessoaCentroCusto> PessoaCentroCustos { get; private set; }
        public ICollection<Movimento> Movimentos { get; private set; }
        public ICollection<ItemMovimento> ItemMovimentos { get; private set; }

        public void Atualizar(string nome)
        {
            Nome = nome;
            Validar();
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O nome do centro de custo não pode ser vazio.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"O nome não ter mais de {NOME_LENGHT} caracteres.");
        }
    }
}
