using Financeiro.Domain.Core.DomainObjects;
using System;

namespace Financeiro.Domain.Entidades
{
    public class ItemMovimento : Entity
    {
        public ItemMovimento(string historico,
                             decimal valor,
                             string chaveLancamento,
                             int numeroParcela,
                             int totalParcela,
                             Guid movimentoId,
                             Guid pessoaId,
                             Guid centroCustoId,
                             Guid? pessoaPagadorId)
        {
            Historico = historico;
            Valor = valor;
            ChaveLancamento = chaveLancamento;
            NumeroParcela = numeroParcela;
            TotalParcela = totalParcela;
            MovimentoId = movimentoId;
            PessoaId = pessoaId;
            CentroCustoId = centroCustoId;
            PessoaPagadorId = pessoaPagadorId;

            Validar();
        }

        protected ItemMovimento() { }

        public const int HISTORICO_LENGHT = 500;
        public const int CHAVE_LANCAMENTO_LENGHT = 100;

        public string Historico { get; private set; }
        public decimal Valor { get; private set; }
        public string ChaveLancamento { get; private set; }
        public int NumeroParcela { get; private set; }
        public int TotalParcela { get; private set; }

        public Guid MovimentoId { get; private set; }
        public Guid PessoaId { get; private set; }
        public Guid CentroCustoId { get; private set; }
        public Guid? PessoaPagadorId { get; private set; }

        public Movimento Movimento { get; private set; }
        public CentroCusto CentroCusto { get; private set; }
        public Pessoa Pessoa { get; private set; }
        public Pessoa PessoaPagador { get; private set; }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Historico, "O historico não pode ser vazio.");
            Validacoes.ValidarTamanho(Historico, HISTORICO_LENGHT, $"O historico não ter mais {HISTORICO_LENGHT} caracteres.");
            Validacoes.ValidarSeIgual(Valor, 0, "O valor não pode ser zerado");
            Validacoes.ValidarSeIgual(NumeroParcela, 0, "A parcela não pode ser zerado");
            Validacoes.ValidarSeIgual(TotalParcela, 0, "O Total de parcela não pode ser zerado");
            Validacoes.ValidarSeIgual(MovimentoId, Guid.Empty, "A movimentação é Obrigatorio");
            Validacoes.ValidarSeIgual(CentroCustoId, Guid.Empty, "O campo centro de custo é Obrigatorio");
            Validacoes.ValidarSeIgual(PessoaId, Guid.Empty, "O campo Pessoa é Obrigatorio.");
        }

        public void Atualizar(string historico,
                              decimal valor,
                              Guid pessoaId,
                              Guid centroCustoId,
                              Guid? pessoaPagadorId)
        {
            Historico = historico;
            Valor = valor;
            PessoaId = pessoaId;
            CentroCustoId = centroCustoId;
            PessoaPagadorId = pessoaPagadorId;

            Validar();
        }



    }
}
