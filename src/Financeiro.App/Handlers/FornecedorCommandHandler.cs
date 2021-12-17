using Financeiro.App.Commands;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Financeiro.App.Handlers
{
    public class FornecedorCommandHandler : HandlerBase,
                                            IRequestHandler<CriarFornecedorCommand, Fornecedor>,
                                            IRequestHandler<AtualizarFornecedorCommand, Fornecedor>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorCommandHandler(IMediatorHandler mediatorHandler, IFornecedorRepository fornecedorRepository) : base(mediatorHandler)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<Fornecedor> Handle(CriarFornecedorCommand request, CancellationToken cancellationToken)
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

                var fornecedor = new Fornecedor(request.Nome);
                _fornecedorRepository.Cadastrar(fornecedor);

                await Commit(_fornecedorRepository.UnitOfWork);

                return fornecedor;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }

        public async Task<Fornecedor> Handle(AtualizarFornecedorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                if (await ValidarSeExisteNomeCadastrado(request.Nome, request.Id))
                {
                    await AdicionarEventError(request.MessageType, "O nome informado já esta sendo utilizado.");
                    return null;
                }

                var fornecedor = await _fornecedorRepository.ObterPorId(request.Id);
                fornecedor.Atualizar(request.Nome);

                _fornecedorRepository.Atualizar(fornecedor);
                await Commit(_fornecedorRepository.UnitOfWork);

                return fornecedor;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }



        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome)
        {
            var fornecedor = await _fornecedorRepository.ObterPorNome(nome);
            return fornecedor != null;
        }

        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome, Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorNome(nome);

            if (fornecedor != null && fornecedor.Id != id)
                return true;

            return false;
        }

    }
}
