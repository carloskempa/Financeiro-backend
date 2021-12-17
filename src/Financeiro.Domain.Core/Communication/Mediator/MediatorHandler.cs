using Financeiro.Domain.Core.Messages;
using MediatR;
using System.Threading.Tasks;

namespace Financeiro.Domain.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<object> EnviarComando<T>(T comando) where T : CommandBase
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }
    }
}
