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
    public class PessoaCommandHandler : HandlerBase,
                                        IRequestHandler<CriarPessoaCommand, Pessoa>,
                                        IRequestHandler<AtualizarPessoaCommand, Pessoa>,
                                        IRequestHandler<DeletarPessoaCommand, bool>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IItemMovimentoRepository _itemMovimentoRepository;

        public PessoaCommandHandler(IMediatorHandler mediatorHandler,
                                    IPessoaRepository pessoaRepository,
                                    IMovimentoRepository movimentoRepository,
                                    IItemMovimentoRepository itemMovimentoRepository)
        : base(mediatorHandler)
        {
            _pessoaRepository = pessoaRepository;
            _movimentoRepository = movimentoRepository;
            _itemMovimentoRepository = itemMovimentoRepository;
        }

        public async Task<Pessoa> Handle(CriarPessoaCommand request, CancellationToken cancellationToken)
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

                var pessoa = new Pessoa(request.Nome);
                _pessoaRepository.Cadastrar(pessoa);

                await Commit(_pessoaRepository.UnitOfWork);

                return pessoa;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }

        public async Task<bool> Handle(DeletarPessoaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return false;

                var pessoa = await _pessoaRepository.ObterPorId(request.Id);

                if (pessoa == null)
                {
                    await AdicionarEventError(request.MessageType, "Pessoa não encontrada.");
                    return false;
                }

                var existeRegistroVinculado = await VerificarSeExisteRegistroVinculado(request);

                if (existeRegistroVinculado)
                    return false;

                var centroCustoPessoas = await _pessoaRepository.Buscar(c => c.PessoaId == request.Id);

                _pessoaRepository.Deletar(pessoa);
                _pessoaRepository.DeletarRangePessoaCentroCusto(centroCustoPessoas);

                return await Commit(_pessoaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return false;
            }
        }

        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome)
        {
            var fornecedor = await _pessoaRepository.ObterPorNome(nome);
            return fornecedor != null;
        }

        private async Task<bool> ValidarSeExisteNomeCadastrado(string nome, Guid id)
        {
            var fornecedor = await _pessoaRepository.ObterPorNome(nome);

            if (fornecedor != null && fornecedor.Id != id)
                return true;

            return false;
        }


        public async Task<bool> VerificarSeExisteRegistroVinculado(DeletarPessoaCommand request)
        {
            var movimentosPessoa = await _movimentoRepository.Buscar(c => c.PessoaId == request.Id || c.PessoaPagadorId == request.Id);

            if (movimentosPessoa.Any())
            {
                await AdicionarEventError(request.MessageType, "Não foi pessovel deletar, existem movimentos financeiros vinculado a esta pessoa.");
                return true;
            }

            var itensMovimento = await _itemMovimentoRepository.Buscar(c => c.PessoaId == request.Id || c.PessoaPagadorId == request.Id);

            if (itensMovimento.Any())
            {
                await AdicionarEventError(request.MessageType, "Não foi pessovel deletar, existem movimentos financeiros vinculado a esta pessoa.");
                return true;
            }

            return false;
        }

        public async Task<Pessoa> Handle(AtualizarPessoaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                var pessoa = await _pessoaRepository.ObterPorId(request.Id);

                if (pessoa == null)
                {
                    await AdicionarEventError(request.MessageType, "Pessoa não encontrada.");
                    return null;
                }

                if (await ValidarSeExisteNomeCadastrado(request.Nome, request.Id))
                {
                    await AdicionarEventError(request.MessageType, "O nome informado já esta sendo utilizado.");
                    return null;
                }

                pessoa.Atualizar(request.Nome);

                if (request.PessoaCentroCustos.Any())
                {
                    _pessoaRepository.DeletarRangePessoaCentroCusto(pessoa.PessoaCentroCustos);
                    _pessoaRepository.CadastrarRange(request.PessoaCentroCustos);
                }
                _pessoaRepository.Atualizar(pessoa);

                await Commit(_pessoaRepository.UnitOfWork);

                return await _pessoaRepository.ObterPorId(request.Id);
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }
    }
}
