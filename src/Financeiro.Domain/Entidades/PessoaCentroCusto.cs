using Financeiro.Domain.Core.DomainObjects;
using System;

namespace Financeiro.Domain.Entidades
{
    public class PessoaCentroCusto
    {
        public PessoaCentroCusto(Guid pessoaId, Guid centroCustoId, decimal valorMensal)
        {
            PessoaId = pessoaId;
            CentroCustoId = centroCustoId;

            Validar();
        }
        protected PessoaCentroCusto() { }

        public Guid PessoaId { get; private set; }
        public Guid CentroCustoId { get; private set; }
        public decimal ValorMensal { get; private set; }
        public Pessoa Pessoa { get; private set; }
        public CentroCusto CentroCusto { get; private set; }


        public void Atualizar(decimal valorMensal)
        {
            ValorMensal = valorMensal;
            Validar();
        }

        private void Validar()
        {
            Validacoes.ValidarSeIgual(PessoaId, Guid.Empty, "O campo Pessoa não pode estar vazio");
            Validacoes.ValidarSeIgual(CentroCustoId, Guid.Empty, "O campo centro custo não pode estar vazio");
        }
    }
}
