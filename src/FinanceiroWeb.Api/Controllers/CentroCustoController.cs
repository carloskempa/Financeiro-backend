using Financeiro.App.Dtos.CentroCusto;
using Financeiro.App.Interfaces;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceiroWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroCustoController : ControllerBase
    {
        private readonly ICentroCustoApp _centroCustoApp;

        public CentroCustoController(ICentroCustoApp centroCustoApp)
        {
            _centroCustoApp = centroCustoApp;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Paginacao paginacao)
        {
            return Ok(await _centroCustoApp.Listar(paginacao));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CentroCustoDto centroCustoDto)
        {
            return Ok(await _centroCustoApp.Cadastrar(centroCustoDto));
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(CentroCustoDto centroCustoDto)
        {
            return Ok(await _centroCustoApp.Atualizar(centroCustoDto));
        }
    }
}
