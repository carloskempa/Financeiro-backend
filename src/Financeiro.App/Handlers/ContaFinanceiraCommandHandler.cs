using Financeiro.App.Commands;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Financeiro.App.Handlers
{
    public class ContaFinanceiraCommandHandler : HandlerBase,
                                                 IRequestHandler<CriarContaFinanceiraCommand, ContaFinanceira>,
                                                 IRequestHandler<AtualizarContaFinanceiraCommand, ContaFinanceira>,
                                                 IRequestHandler<DeletarContaFinanceiraCommand, bool>
    {
        private readonly IContaFinanceiraRepository _contaFinanceiraRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public ContaFinanceiraCommandHandler(IMediatorHandler mediatorHandler,
                                             IContaFinanceiraRepository contaFinanceiraRepository,
                                             IMovimentoRepository movimentoRepository)
        : base(mediatorHandler)
        {
            _contaFinanceiraRepository = contaFinanceiraRepository;
            _movimentoRepository = movimentoRepository;
        }

        public async Task<ContaFinanceira> Handle(CriarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                var contaFinanceira = new ContaFinanceira(request.Nome);
                _contaFinanceiraRepository.Cadastrar(contaFinanceira);

                await Commit(_contaFinanceiraRepository.UnitOfWork);

                return contaFinanceira;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }

        public async Task<ContaFinanceira> Handle(AtualizarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                var contaFinanceira = await _contaFinanceiraRepository.ObterPorId(request.Id);

                if (contaFinanceira == null)
                {
                    await AdicionarEventError(request.MessageType, "Conta Financeira não encontrado.");
                    return null;
                }

                contaFinanceira.Atualizar(request.Nome);

                _contaFinanceiraRepository.Atualizar(contaFinanceira);
                await Commit(_contaFinanceiraRepository.UnitOfWork);

                return contaFinanceira;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }

        public async Task<bool> Handle(DeletarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return false;

                var contaFinanceira = await _contaFinanceiraRepository.ObterPorId(request.Id);

                if (contaFinanceira == null)
                {
                    await AdicionarEventError(request.MessageType, "Conta Financeira não encontrado.");
                    return false;
                }

                var movimentosVinculados = await VerificarSeExisteMovimentosFinanceirosVinculados(request);

                if (movimentosVinculados)
                    return false;

                _contaFinanceiraRepository.Deletar(contaFinanceira);

                return await Commit(_contaFinanceiraRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return false;
            }
        }

        private async Task<bool> VerificarSeExisteMovimentosFinanceirosVinculados(DeletarContaFinanceiraCommand request)
        {
            var movimentos = await _movimentoRepository.Buscar(c => c.ContaId == request.Id);
            if (movimentos.Any())
            {
                await AdicionarEventError(request.MessageType, "Conta Financeira não pode ser deletada por existir movimentos financeiros vinculado.");
                return true;
            }

            return false;
        }

    }
}
