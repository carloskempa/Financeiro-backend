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
    public class FornecedorApp : AppBase, IFornecedorApp
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorQuery _fornecedorQuery;
        public FornecedorApp(IMediatorHandler mediatorHandler,
                             IMapper mapper,
                             INotificationHandler<DomainNotification> notifications,
                             IFornecedorRepository fornecedorRepository, 
                             IFornecedorQuery fornecedorQuery)
        : base(mediatorHandler, mapper, notifications)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorQuery = fornecedorQuery;
        }

        public async Task<PaginatedRest<Fornecedor>> Listar(Paginacao paginacao)
        {
            return await _fornecedorQuery.ListarTodos(paginacao);
        }

        public async Task<RetornoPadrao<FornecedorDto>> ObterPorId(Guid id)
        {
            return Sucesso(_mapper.Map<FornecedorDto>(await _fornecedorRepository.ObterPorId(id)));
        }

        public async Task<RetornoPadrao<FornecedorDto>> Cadastrar(FornecedorDto fornecedor)
        {
            var comando = new CriarFornecedorCommand(fornecedor.Nome);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<FornecedorDto>(ObterMensagensErro);

            var FormecedorReturn = _mapper.Map<FornecedorDto>(resultado);

            return Sucesso(FormecedorReturn, "Fornecedor cadastrado com sucesso!");
        }

        public async Task<RetornoPadrao<FornecedorDto>> Atualizar(FornecedorDto fornecedor)
        {
            var comando = new AtualizarFornecedorCommand(fornecedor.Id,fornecedor.Nome);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<FornecedorDto>(ObterMensagensErro);

            var FormecedorReturn = _mapper.Map<FornecedorDto>(resultado);

            return Sucesso(FormecedorReturn, "Fornecedor atualizado com sucesso!");
        }

        public async Task<RetornoPadrao<FornecedorDto>> Deletar(Guid id)
        {
            var comando = new DeletarFornecedorCommand(id);
            await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<FornecedorDto>(ObterMensagensErro);

            return Sucesso<FornecedorDto>("Fornecedor deletado com sucesso!");
        }

        public async Task<IEnumerable<FornecedorDto>> ListarTodos()
        {
            return _mapper.Map<IEnumerable<FornecedorDto>>(await _fornecedorQuery.ListarTodos());
        }
    }
}
