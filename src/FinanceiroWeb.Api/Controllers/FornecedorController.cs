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
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorApp _fornecedorApp;

        public FornecedorController(IFornecedorApp fornecedorApp)
        {
            _fornecedorApp = fornecedorApp;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Paginacao paginacao)
        {
            return Ok(await _fornecedorApp.Listar(paginacao));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            return Ok(await _fornecedorApp.ObterPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(FornecedorDto fornecedorDto)
        {
            return Ok(await _fornecedorApp.Cadastrar(fornecedorDto));
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(FornecedorDto fornecedorDto)
        {
            return Ok(await _fornecedorApp.Atualizar(fornecedorDto));
        }
    }
}
