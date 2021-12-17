using Financeiro.App.Commands;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Financeiro.App.Handlers
{
    public class PessoaCommandHandler : HandlerBase,
                                        IRequestHandler<CriarPessoaCommand, Pessoa>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaCommandHandler(IMediatorHandler mediatorHandler, IPessoaRepository pessoaRepository) : base(mediatorHandler)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<Pessoa> Handle(CriarPessoaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                if (await ValidarSeExisteNomeCadastrado(request.Nome))
                {
                    await AdicionarEventError(request.MessageType, "O nome informado já esta sendo utilizado.");
                    return null;
                }

                var pessoa = new Pessoa(request.Nome);
                _pessoaRepository.Cadastrar(pessoa);

                await Commit(_pessoaRepository.UnitOfWork);

                return pessoa;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }

        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome)
        {
            var fornecedor = await _pessoaRepository.ObterPorNome(nome);
            return fornecedor != null;
        }

        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome, Guid id)
        {
            var fornecedor = await _pessoaRepository.ObterPorNome(nome);

            if (fornecedor != null && fornecedor.Id != id)
                return true;

            return false;
        }
    }
}
