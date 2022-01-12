using Financeiro.App.Dtos.CentroCusto;
using Financeiro.App.Interfaces;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            return Ok(await _centroCustoApp.ObterPorId(id));
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Paginacao paginacao)
        {
            return Ok(await _centroCustoApp.Listar(paginacao));
        }

        [HttpGet("obterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _centroCustoApp.ListarTodos());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CentroCustoDto centroCustoDto)
        {
            var resultado = await _centroCustoApp.Cadastrar(centroCustoDto);
            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(CentroCustoDto centroCustoDto)
        {
            var resultado = await _centroCustoApp.Atualizar(centroCustoDto);

            if (!resultado.Sucesso)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var resultado = await _centroCustoApp.Deletar(id);

            if (!resultado.Sucesso)
                return BadRequest(resultado);

            return Ok(resultado);
        }

    }
}
