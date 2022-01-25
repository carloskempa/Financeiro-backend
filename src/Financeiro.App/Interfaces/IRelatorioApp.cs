using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.DataTransferObjects.Relatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IRelatorioApp
    {
        Task<IEnumerable<RelatorioResumoMesAMes>> ObterResumoMesAMes(RelatorioFilter filter);
    }
}
