using Financeiro.App.Interfaces;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceiroWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioApp _relatorioApp;

        public RelatorioController(IRelatorioApp relatorioApp)
        {
            _relatorioApp = relatorioApp;
        }

        [HttpGet("resumoMesAMes")]
        public async Task<IActionResult> ObterRelatorioMesAMes([FromQuery] RelatorioFilter filter)
        {
            return Ok(await _relatorioApp.ObterResumoMesAMes(filter));
        }

    }
}
