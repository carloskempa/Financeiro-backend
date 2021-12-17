using Financeiro.App.Commands;
using Financeiro.App.Dtos;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using Financeiro.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Financeiro.App.Handlers
{
    public class UsuarioCommandHandler : HandlerBase,
                                         IRequestHandler<CriarUsuarioCommand, Usuario>,
                                         IRequestHandler<LogarUsuarioCommand, Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICriptografiaService _criptografiaService;
        public UsuarioCommandHandler(IMediatorHandler mediatorHandler, IUsuarioRepository usuarioRepository, ICriptografiaService criptografiaService) : base(mediatorHandler)
        {
            _usuarioRepository = usuarioRepository;
            _criptografiaService = criptografiaService;
        }

        public async Task<Usuario> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                var emailExisteCadastrado = await VeridicarSeLoginJaExiste(request.Login);

                if (emailExisteCadastrado)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Login informado já cadastrado."));
                    return null;
                }

                var senhaUsuario = _criptografiaService.Encrypt(request.Senha);
                var usuario = new Usuario(request.Nome, request.Login, senhaUsuario, true);

                _usuarioRepository.Cadastrar(usuario);
                await Commit(_usuarioRepository.UnitOfWork);

                return usuario;

            }
            catch (Exception ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, ex.Message));
                return null;
            }
        }

        public async Task<Usuario> Handle(LogarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                var usuario = await _usuarioRepository.ObterPorLogin(request.Login);

                if (usuario == null)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "usuário e senha inválido"));
                    return null;
                }

                var senhaUsuario = _criptografiaService.Encrypt(request.Senha);

                if (!usuario.Senha.Equals(senhaUsuario))
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "usuário e senha inválido"));
                    return null;
                }

                usuario.CriarRefreshToken();
                _usuarioRepository.Atualizar(usuario);

                await Commit(_usuarioRepository.UnitOfWork);

                return usuario;
            }
            catch (Exception ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, ex.Message));
                return null;
            }
        }

        private async Task<bool> VeridicarSeLoginJaExiste(string login)
        {
            var usuario = await _usuarioRepository.ObterPorLogin(login);
            return usuario != null;
        }

        private async Task<bool> VeridicarSeLoginJaExiste(string login, Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorLogin(login);

            if (usuario != null && usuario.Id != usuarioId)
                return true;

            return false;
        }

    }
}
