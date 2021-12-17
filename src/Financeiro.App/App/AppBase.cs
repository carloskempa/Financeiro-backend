using AutoMapper;
using Financeiro.App.Dtos;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financeiro.App.App
{
    public abstract class AppBase
    {
        protected readonly IMediatorHandler _mediatorHandler;
        protected readonly IMapper _mapper;
        private readonly DomainNotificationHandler _notifications;
        protected AppBase(IMediatorHandler mediatorHandler, IMapper mapper, INotificationHandler<DomainNotification> notifications)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }
        protected List<string> ObterMensagensErro => ObterMensagensErros();

        private List<string> ObterMensagensErros()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }
        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected RetornoPadrao<T> Sucesso<T>(T data) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = data,
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(T data, string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = data,
                Mensagens = new List<string> { mensagem }
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(T data, List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = data,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = default,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = default,
                Mensagens = new List<string> { mensagem }
            };
        }


        protected RetornoPadrao<T> Error<T>(T data) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = data,
            };
        }

        protected RetornoPadrao<T> Error<T>(T data, string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = data,
                Mensagens = new List<string> { mensagem }
            };
        }

        protected RetornoPadrao<T> Error<T>(T data, List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = data,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Error<T>(List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = default,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Error<T>(string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = default,
                Mensagens = new List<string> { mensagem }
            };
        }


    }
}
