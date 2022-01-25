using AutoMapper;
using Financeiro.App.Interfaces;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.DataTransferObjects.Relatorio;
using Financeiro.Domain.Interfaces.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.App.App
{
    public class RelatorioApp : AppBase, IRelatorioApp
    {
        private readonly IRelatorioQuery _relatorioQuery;
        public RelatorioApp(IMediatorHandler mediatorHandler, 
                            IMapper mapper, 
                            INotificationHandler<DomainNotification> notifications, 
                            IRelatorioQuery relatorioQuery) 
        : base(mediatorHandler, mapper, notifications)
        {
            _relatorioQuery = relatorioQuery;
        }

        public async Task<IEnumerable<RelatorioResumoMesAMes>> ObterResumoMesAMes(RelatorioFilter filter)
        {
            return await _relatorioQuery.ObterResumoMesAMes(filter);
        }
    }
}
