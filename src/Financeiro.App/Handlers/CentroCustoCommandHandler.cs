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
    public class CentroCustoCommandHandler : HandlerBase,
                                      IRequestHandler<CriarCentroCustoCommand, CentroCusto>,
                                      IRequestHandler<AtualizarCentroCustoCommand, CentroCusto>,
                                      IRequestHandler<DeletarCentroCustoCommand, bool>
    {
        private readonly ICentroCustoRepository _centroCustoRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IItemMovimentoRepository _itemMovimentoRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public CentroCustoCommandHandler(IMediatorHandler mediatorHandler,
                                         ICentroCustoRepository centroCustoRepository,
                                         IMovimentoRepository movimentoRepository,
                                         IItemMovimentoRepository itemMovimentoRepository,
                                         IPessoaRepository pessoaRepository)
        : base(mediatorHandler)
        {
            _centroCustoRepository = centroCustoRepository;
            _movimentoRepository = movimentoRepository;
            _itemMovimentoRepository = itemMovimentoRepository;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<CentroCusto> Handle(CriarCentroCustoCommand request, CancellationToken cancellationToken)
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

        public async Task<bool> Handle(DeletarCentroCustoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return false;

                var centroCusto = await _centroCustoRepository.ObterPorId(request.Id);

                if (centroCusto == null)
                {
                    await AdicionarEventError(request.MessageType, "Centro de custo não encontrado");
                    return false;
                }

                var podeExcluir = true;

                podeExcluir = podeExcluir == true ? await ValidarSeExisteMovimentosVinculado(request) : podeExcluir;
                podeExcluir = podeExcluir == true ? await ValidarSeExisteItensMovimentosVinculado(request) : podeExcluir;
                podeExcluir = podeExcluir == true ? await ValidarSeExistePessoasVinculado(request) : podeExcluir;

                if (!podeExcluir)
                    return false;

                _centroCustoRepository.Deletar(centroCusto);

                return await Commit(_centroCustoRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return false;
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
        private async Task<bool> ValidarSeExisteMovimentosVinculado(DeletarCentroCustoCommand command)
        {
            var movimentos = await _movimentoRepository.Buscar(c => c.CentroCustoId == command.Id);

            if (movimentos.Any())
            {
                await AdicionarEventError(command.MessageType, "Centro de custo nao pode ser deletado por existir movimentos vinculados");
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarSeExisteItensMovimentosVinculado(DeletarCentroCustoCommand command)
        {
            var movimentos = await _itemMovimentoRepository.Buscar(c => c.CentroCustoId == command.Id);

            if (movimentos.Any())
            {
                await AdicionarEventError(command.MessageType, "Centro de custo nao pode ser deletado por existir itens de movimentos vinculados");
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarSeExistePessoasVinculado(DeletarCentroCustoCommand command)
        {
            var movimentos = await _pessoaRepository.Buscar(c => c.CentroCustoId == command.Id);

            if (movimentos.Any())
            {
                await AdicionarEventError(command.MessageType, "Centro de custo nao pode ser deletado por existir Pessoas vinculados");
                return false;
            }
            return true;
        }
    }
}
