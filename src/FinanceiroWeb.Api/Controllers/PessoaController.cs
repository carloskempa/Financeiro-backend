using Financeiro.App.Dtos.Pessoa;
using Financeiro.App.Interfaces;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FinanceiroWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaApp _pessoaApp;

        public PessoaController(IPessoaApp pessoaApp)
        {
            _pessoaApp = pessoaApp;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Paginacao paginacao)
        {
            return Ok(await _pessoaApp.Listar(paginacao));
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            return Ok(await _pessoaApp.ObterPorId(id));
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(PessoaCadastroDto pessoaCadastroDto)
        {
            var result = await _pessoaApp.Cadastrar(pessoaCadastroDto);

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
