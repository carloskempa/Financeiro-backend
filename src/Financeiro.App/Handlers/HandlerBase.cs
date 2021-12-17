using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Core.Messages;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.Handlers
{
    public abstract class HandlerBase
    {
        protected readonly IMediatorHandler _mediatorHandler;

        public HandlerBase(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }


        protected async Task AdicionarEventError(string key, string value)
        {
            await _mediatorHandler.PublicarNotificacao(new DomainNotification(key, value));
        }

        protected bool ValidarComando(CommandBase message)
        {
            if (message.EhValido())
                return true;

            foreach (var error in message.ValidationResult.Errors)
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }


        protected async Task<bool> Commit(IUnitOfWork unitOfWork)
        {
            return await unitOfWork.Commit();
        }

        protected async Task TratarExeception(Exception ex, CommandBase request)
        {
            await AdicionarEventError(request.MessageType, ex.Message);
        }


    }
}
