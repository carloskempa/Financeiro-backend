using Financeiro.App.Dtos;
using Financeiro.App.Interfaces;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceiroWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaFinanceiraController : ControllerBase
    {
        private readonly IContaFinanceiraApp _contaFinanceiraApp;

        public ContaFinanceiraController(IContaFinanceiraApp contaFinanceiraApp)
        {
            _contaFinanceiraApp = contaFinanceiraApp;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Paginacao paginacao)
        {
            return Ok(await _contaFinanceiraApp.Listar(paginacao));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ContaFinanceiraDto contaFinanceira)
        {
            return Ok(await _contaFinanceiraApp.Cadastrar(contaFinanceira));
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(ContaFinanceiraDto contaFinanceira)
        {
            return Ok(await _contaFinanceiraApp.Atualizar(contaFinanceira));
        }
    }
}
