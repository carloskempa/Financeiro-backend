using Financeiro.App.Dtos;
using Financeiro.App.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FinanceiroWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApp _usuarioApp;

        public UsuarioController(IUsuarioApp usuarioApp)
        {
            _usuarioApp = usuarioApp;
        }

        [HttpGet("id:Guid")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            return Ok(await _usuarioApp.ObterPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioDto usuarioDto)
        {
            return Ok(await _usuarioApp.Cadastrar(usuarioDto));
        }

        [HttpPost("logar")]
        public async Task<IActionResult> Logar(UsuarioLogin login)
        {
            return Ok(await _usuarioApp.Logar(login));
        }
    }
}
