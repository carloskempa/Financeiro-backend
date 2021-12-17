using AutoMapper;
using Canducci.Pagination;
using Financeiro.App.Commands;
using Financeiro.App.Dtos;
using Financeiro.App.Dtos.Pessoa;
using Financeiro.App.Interfaces;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Queries;
using Financeiro.Domain.Interfaces.Respositories;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.App
{
    public class PessoaApp : AppBase, IPessoaApp
    {
        private readonly IPessoaRepository _pessoRepository;
        private readonly IPessoaQuery _pessoaQuery;
        public PessoaApp(IMediatorHandler mediatorHandler,
                         IMapper mapper,
                         INotificationHandler<DomainNotification> notifications,
                         IPessoaRepository pessoRepository, 
                         IPessoaQuery pessoaQuery)
        : base(mediatorHandler, mapper, notifications)
        {
            _pessoRepository = pessoRepository;
            _pessoaQuery = pessoaQuery;
        }
        public async Task<PaginatedRest<Pessoa>> Listar(Paginacao paginacao)
        {
            return await _pessoaQuery.ListarTodos(paginacao);
        }

        public async Task<RetornoPadrao<PessoaDto>> ObterPorId(Guid id)
        {
            var pessoa = await _pessoRepository.ObterPorId(id);
            return Sucesso(_mapper.Map<PessoaDto>(pessoa));

        }

        public Task<RetornoPadrao<PessoaCadastroDto>> Atualizar(PessoaCadastroDto pessoa)
        {
            throw new NotImplementedException();
        }

        public async Task<RetornoPadrao<PessoaCadastroDto>> Cadastrar(PessoaCadastroDto pessoa)
        {
            var comando = new CriarPessoaCommand(pessoa.Nome);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<PessoaCadastroDto>(ObterMensagensErro);

            var pessoaDto = _mapper.Map<PessoaCadastroDto>(resultado);

            return Sucesso(pessoaDto, "Pessoa cadastrada com sucesso");
        }
    }
}
