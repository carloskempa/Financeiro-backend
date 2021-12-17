using AutoMapper;
using Financeiro.App.Commands;
using Financeiro.App.Dtos;
using Financeiro.App.Interfaces;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Interfaces.Queries;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.App
{
    public class MovimentoApp : AppBase, IMovimentoApp
    {
        private readonly IMovimentoQuery _movimentoQuery;
        public MovimentoApp(IMediatorHandler mediatorHandler,
                            IMapper mapper,
                            INotificationHandler<DomainNotification> notifications,
                            IMovimentoQuery movimentoQuery)
        : base(mediatorHandler, mapper, notifications)
        {
            _movimentoQuery = movimentoQuery;
        }

        public async Task<RetornoPadrao<MovimentoDto>> Cadastrar(MovimentoDto movimentoDto)
        {
            var comando = new CriarMovimentoCommand(movimentoDto.Descricao,
                                                    movimentoDto.ValorMovimento,
                                                    movimentoDto.Observacao,
                                                    movimentoDto.IsPago,
                                                    movimentoDto.DataVencimento,
                                                    movimentoDto.DataMovimento,
                                                    movimentoDto.TipoMovimento,
                                                    movimentoDto.ContaId,
                                                    movimentoDto.CentroCustoId,
                                                    movimentoDto.FornecedorId,
                                                    movimentoDto.PessoaId,
                                                    movimentoDto.PessoaPagadorId);

            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<MovimentoDto>(ObterMensagensErro);

            return Sucesso(_mapper.Map<MovimentoDto>(resultado), "Movimento cadastrado com sucesso!");
        }

        public async Task<TabelaMovimentacao> ObterMovimentacoes(MovimentoFilter filter, Paginacao paginacao)
        {
            if (filter.Ano == 0 || filter.Mes == 0)
                throw new Exception("Informe o mês e ano para filtrar as movimentaçoes");

            var movimentos = await _movimentoQuery.MovimentoFilter(filter, paginacao);

            return new TabelaMovimentacao() { Data = movimentos };
        }
    }
}
