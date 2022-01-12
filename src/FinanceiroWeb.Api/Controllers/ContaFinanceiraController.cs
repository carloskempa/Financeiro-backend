using Financeiro.App.Dtos;
using Financeiro.App.Interfaces;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            return Ok(await _contaFinanceiraApp.ObterPorId(id));
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Paginacao paginacao)
        {
            return Ok(await _contaFinanceiraApp.Listar(paginacao));
        }

        [HttpGet("obterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _contaFinanceiraApp.ListarTodos());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ContaFinanceiraDto contaFinanceira)
        {
            var resultado = await _contaFinanceiraApp.Cadastrar(contaFinanceira);

            if (!resultado.Sucesso)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] ContaFinanceiraDto contaFinanceira)
        {
            var resultado = await _contaFinanceiraApp.Atualizar(contaFinanceira);

            if (!resultado.Sucesso)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var resultado = await _contaFinanceiraApp.Deletar(id);

            if (!resultado.Sucesso)
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
