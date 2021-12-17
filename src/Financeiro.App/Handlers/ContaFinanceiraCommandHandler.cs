using Financeiro.App.Commands;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
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
    public class ContaFinanceiraCommandHandler : HandlerBase,
                                                 IRequestHandler<CriarContaFinanceiraCommand, ContaFinanceira>,
                                                 IRequestHandler<AtualizarContaFinanceiraCommand, ContaFinanceira>
    {
        private readonly IContaFinanceiraRepository _contaFinanceiraRepository;
        public ContaFinanceiraCommandHandler(IMediatorHandler mediatorHandler, IContaFinanceiraRepository contaFinanceiraRepository) : base(mediatorHandler)
        {
            _contaFinanceiraRepository = contaFinanceiraRepository;
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

                if(contaFinanceira == null)
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
    }
}
