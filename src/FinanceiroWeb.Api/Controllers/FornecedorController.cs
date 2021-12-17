using Financeiro.App.Dtos;
using Financeiro.App.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return Ok("Tudo Certo");
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
