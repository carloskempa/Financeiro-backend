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
                                            IRequestHandler<AtualizarFornecedorCommand, Fornecedor>,
                                            IRequestHandler<DeletarFornecedorCommand, bool>
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public FornecedorCommandHandler(IMediatorHandler mediatorHandler,
                                        IFornecedorRepository fornecedorRepository,
                                        IMovimentoRepository movimentoRepository)
        : base(mediatorHandler)
        {
            _fornecedorRepository = fornecedorRepository;
            _movimentoRepository = movimentoRepository;
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

        public async Task<bool> Handle(DeletarFornecedorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return false;

                var fornecedor = await _fornecedorRepository.ObterPorId(request.Id);

                if (fornecedor == null)
                {
                    await AdicionarEventError(request.MessageType, "Fornecedor não encontrado.");
                    return false;
                }

                var existeMovimentos = await ValidarSeExisteMovimentoFinanceiroVinculado(request);

                if (existeMovimentos)
                    return false;

                _fornecedorRepository.Deletar(fornecedor);
                return await Commit(_fornecedorRepository.UnitOfWork);

            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return false;
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

        private async Task<bool> ValidarSeExisteMovimentoFinanceiroVinculado(DeletarFornecedorCommand request)
        {
            var movimentos = await _movimentoRepository.Buscar(c => c.FornecedorId == request.Id);

            if (movimentos.Any())
            {
                await AdicionarEventError(request.MessageType, "Fornecedor não pode ser deletado por existir movimentos financeiros vinculados.");
                return true;
            }

            return false;
        }
    }
}
