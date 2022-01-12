using AutoMapper;
using Canducci.Pagination;
using Financeiro.App.Commands;
using Financeiro.App.Dtos;
using Financeiro.App.Dtos.CentroCusto;
using Financeiro.App.Interfaces;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Queries;
using Financeiro.Domain.Interfaces.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financeiro.App.App
{
    public class CentroCustoApp : AppBase, ICentroCustoApp
    {
        private readonly ICentroCustoRepository _centroCustoRepository;
        private readonly ICentroCustoQuery _centroCustoQuery;
        public CentroCustoApp(IMediatorHandler mediatorHandler,
                              IMapper mapper,
                              INotificationHandler<DomainNotification> notifications,
                              ICentroCustoRepository centroCustoRepository,
                              ICentroCustoQuery centroCustoQuery)
        : base(mediatorHandler, mapper, notifications)
        {
            _centroCustoRepository = centroCustoRepository;
            _centroCustoQuery = centroCustoQuery;
        }
        public async Task<PaginatedRest<CentroCusto>> Listar(Paginacao paginacao)
        {
            return await _centroCustoQuery.ListarTodos(paginacao);
        }

        public async Task<RetornoPadrao<CentroCustoDto>> ObterPorId(Guid id)
        {
            var centroCusto = await _centroCustoRepository.ObterPorId(id);
            return Sucesso(_mapper.Map<CentroCustoDto>(centroCusto));
        }

        public async Task<RetornoPadrao<CentroCustoDto>> Cadastrar(CentroCustoDto centroCusto)
        {
            var comando = new CriarCentroCustoCommand(centroCusto.Nome);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<CentroCustoDto>(ObterMensagensErro);

            var centroCustoReturn = _mapper.Map<CentroCustoDto>(resultado);

            return Sucesso(centroCustoReturn, "Centro custo cadastrado com sucesso!");
        }

        public async Task<RetornoPadrao<CentroCustoDto>> Atualizar(CentroCustoDto centroCusto)
        {
            var comando = new AtualizarCentroCustoCommand(centroCusto.Id, centroCusto.Nome);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<CentroCustoDto>(ObterMensagensErro);

            var centroCustoReturn = _mapper.Map<CentroCustoDto>(resultado);

            return Sucesso(centroCustoReturn, "Centro custo atualizado com sucesso!");
        }

        public async Task<RetornoPadrao<CentroCustoDto>> Deletar(Guid id)
        {
            var comando = new DeletarCentroCustoCommand(id);
            await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<CentroCustoDto>(ObterMensagensErro);

            return Sucesso<CentroCustoDto>("Centro custo deletado com sucesso!");
        }

        public async Task<IEnumerable<CentroCustoDto>> ListarTodos()
        {
            return _mapper.Map<IEnumerable<CentroCustoDto>>(await _centroCustoQuery.ListarTodos());
        }
    }
}
