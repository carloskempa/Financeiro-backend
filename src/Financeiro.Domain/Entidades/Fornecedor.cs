using Financeiro.Domain.Core.DomainObjects;
using System.Collections.Generic;

namespace Financeiro.Domain.Entidades
{
    public class Fornecedor : Entity
    {
        public Fornecedor(string nome)
        {
            Nome = nome;
            Validar();
        }
        protected Fornecedor() { }

        public const int NOME_LENGHT = 100;

        public string Nome { get; private set; }
        public ICollection<Movimento> Movimentos { get; private set; }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O nome do fornecedor não pode ser vazio.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"O nome do fornecedor não pode ter mais de {NOME_LENGHT} caracteres.");
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
            Validar();
        }

    }
}
