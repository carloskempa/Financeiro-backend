using Financeiro.Domain.Core.DomainObjects;
using System.Collections.Generic;

namespace Financeiro.Domain.Entidades
{
    public class Pessoa : Entity
    {
        public Pessoa(string nome)
        {
            Nome = nome;
        }

        protected Pessoa() { }

        public const int NOME_LENGHT = 100;

        public string Nome { get; private set; }
        public ICollection<PessoaCentroCusto> PessoaCentroCustos { get; private set; }
        public ICollection<Movimento> Movimentos { get; private set; }
        public ICollection<Movimento> MovimentosPagador { get; private set; }
        public ICollection<ItemMovimento> ItensMovimentos { get; private set; }
        public ICollection<ItemMovimento> ItensMovimentosPagador { get; private set; }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O nome da pessoa não pode ser vazio.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"O nome da pessoa não pode ter mais de {NOME_LENGHT} caracteres.");
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
            Validar();
        }
    }
}
