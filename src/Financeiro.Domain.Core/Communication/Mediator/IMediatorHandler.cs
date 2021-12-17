using Financeiro.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Domain.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task<object> EnviarComando<T>(T comando) where T : CommandBase;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
