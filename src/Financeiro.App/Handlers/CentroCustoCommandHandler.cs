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
    public class CentroCustoCommandHandler : HandlerBase,
                                      IRequestHandler<CriarCentroCustoCommand, CentroCusto>,
                                      IRequestHandler<AtualizarCentroCustoCommand, CentroCusto>
    {
        private readonly ICentroCustoRepository _centroCustoRepository;
        public CentroCustoCommandHandler(IMediatorHandler mediatorHandler, ICentroCustoRepository centroCustoRepository) : base(mediatorHandler)
        {
            _centroCustoRepository = centroCustoRepository;
        }

        public async Task<CentroCusto> Handle(CriarCentroCustoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                if(await ValidarSeExisteNomeCadastrado(request.Nome))
                {
                    await AdicionarEventError(request.MessageType, "O nome informado já esta sendo utilizado.");
                    return null;
                }

                var centroCusto = new CentroCusto(request.Nome);
                _centroCustoRepository.Cadastrar(centroCusto);

                await Commit(_centroCustoRepository.UnitOfWork);

                return centroCusto;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }

        public async Task<CentroCusto> Handle(AtualizarCentroCustoCommand request, CancellationToken cancellationToken)
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

                var centroCusto = await _centroCustoRepository.ObterPorId(request.Id);
                centroCusto.Atualizar(request.Nome);

                _centroCustoRepository.Atualizar(centroCusto);

                await Commit(_centroCustoRepository.UnitOfWork);

                return centroCusto;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }


        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome)
        {
            var centroCusto = await _centroCustoRepository.ObterPeloNome(nome);
            return centroCusto != null;
        }
        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome, Guid id)
        {
            var centroCusto = await _centroCustoRepository.ObterPeloNome(nome);

            if (centroCusto != null && centroCusto.Id != id)
                return true;

            return false;
        }

    }
}
