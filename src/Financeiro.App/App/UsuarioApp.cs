using AutoMapper;
using Financeiro.App.Commands;
using Financeiro.App.Dtos;
using Financeiro.App.Interfaces;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Interfaces.Respositories;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.App
{
    public class UsuarioApp : AppBase, IUsuarioApp
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioApp(IMediatorHandler mediatorHandler,
                          IMapper mapper,
                          INotificationHandler<DomainNotification> notifications,
                          IUsuarioRepository usuarioRepository)
        : base(mediatorHandler, mapper, notifications)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RetornoPadrao<UsuarioDto>> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            return Sucesso(_mapper.Map<UsuarioDto>(usuario));
        }

        public async Task<RetornoPadrao<UsuarioDto>> Cadastrar(UsuarioDto usuario)
        {
            var comando = new CriarUsuarioCommand(usuario.Nome, usuario.Login, usuario.Senha, usuario.Ativo);
            await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<UsuarioDto>(ObterMensagensErro);

            return Sucesso<UsuarioDto>("Usuário cadastrado com sucesso!");
        }

        public async Task<RetornoPadrao<UsuarioDto>> Logar(UsuarioLogin usuario)
        {
            var comando = new LogarUsuarioCommand(usuario.Login, usuario.Senha);
            var resultado = await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
                return Error<UsuarioDto>(ObterMensagensErro);

            var usuarioRetorno = _mapper.Map<UsuarioDto>(resultado);

            return Sucesso(usuarioRetorno, "Usuário logado com sucesso!");
        }

    }
}
