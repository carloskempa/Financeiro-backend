using AutoMapper;
using Canducci.Pagination;
using Financeiro.App.Commands;
using Financeiro.App.Dtos;
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
    public class ContaFinanceiraApp : AppBase, IContaFinanceiraApp
    {
        private readonly IContaFinanceiraRepository _contaFinanceiraRepository;
        private readonly IContaFinanceiraQuery _contaFinanceiraQuery;
        public ContaFinanceiraApp(IMediatorHandler mediatorHandler,
                                  IMapper mapper,
                                  INotificationHandler<DomainNotification> notifications,
                                  IContaFinanceiraRepository contaFinanceiraRepository, 
                                  IContaFinanceiraQuery contaFinanceiraQuery)
        : base(mediatorHandler, mapper, notifications)
        {
            _contaFinanceiraRepository = contaFinanceiraRepository;
            _contaFinanceiraQuery = contaFinanceiraQuery;
        }
        public async Task<PaginatedRest<ContaFinanceira>> Listar(Paginacao paginacao)
        {
            return await _contaFinanceiraQuery.ListarTodos(paginacao);
        }

        public async Task<RetornoPadrao<ContaFinanceiraDto>> ObterPorId(Guid id)
        {
            return Sucesso(_mapper.Map<ContaFinanceiraDto>(await _contaFinanceiraRepository.ObterPorId(id)));
        }

        public async Task<RetornoPadrao<ContaFinanceiraDto>> Cadastrar(ContaFinanceiraDto contaFinanceira)
        {
            var comando = new CriarContaFinanceiraCommand(contaFinanceira.Nome);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<ContaFinanceiraDto>(ObterMensagensErro);

            var contaFinanceidaDto = _mapper.Map<ContaFinanceiraDto>(resultado);

            return Sucesso(contaFinanceidaDto, "Conta cadastrada com sucesso!");
        }

        public async Task<RetornoPadrao<ContaFinanceiraDto>> Atualizar(ContaFinanceiraDto contaFinanceira)
        {
            var comando = new AtualizarContaFinanceiraCommand(contaFinanceira.Id, contaFinanceira.Nome);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<ContaFinanceiraDto>(ObterMensagensErro);

            var contaFinanceidaDto = _mapper.Map<ContaFinanceiraDto>(resultado);

            return Sucesso(contaFinanceidaDto, "Conta atualizada com sucesso!");
        }

        public async Task<RetornoPadrao<ContaFinanceiraDto>> Deletar(Guid id)
        {
            var comando = new DeletarContaFinanceiraCommand(id); 
            await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<ContaFinanceiraDto>(ObterMensagensErro);

            return Sucesso<ContaFinanceiraDto>("Conta deletado com sucesso!");
        }

        public async Task<IEnumerable<ContaFinanceiraDto>> ListarTodos()
        {
            return _mapper.Map<IEnumerable<ContaFinanceiraDto>>(await _contaFinanceiraQuery.ListarTodos());
        }
    }
}
