using Financeiro.App.Commands;
using Financeiro.Domain.Core.Communication.Mediator;
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
    public class MovimentoCommandHandler : HandlerBase,
                                           IRequestHandler<CriarMovimentoCommand, Movimento>
    {
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoCommandHandler(IMediatorHandler mediatorHandler, IMovimentoRepository movimentoRepository) : base(mediatorHandler)
        {
            _movimentoRepository = movimentoRepository;
        }

        public async Task<Movimento> Handle(CriarMovimentoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request))
                    return null;

                var movimento = new Movimento(request.Descricao,
                                              request.ValorMovimento,
                                              request.Observacao,
                                              request.IsPago,
                                              request.DataVencimento,
                                              request.DataMovimento,
                                              request.TipoMovimento,
                                              request.ContaId,
                                              request.CentroCustoId,
                                              request.FornecedorId,
                                              request.PessoaId,
                                              request.PessoaPagadorId);


                _movimentoRepository.Cadastrar(movimento);

                var result = await Commit(_movimentoRepository.UnitOfWork);

                if (!result)
                    await AdicionarEventError(request.MessageType, "Erro ao cadastrar o Movimento");

                return movimento;
            }
            catch (Exception ex)
            {
                await TratarExeception(ex, request);
                return null;
            }
        }
    }
}
