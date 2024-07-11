using LocadoraFilmes.API.Extensions;
using LocadoraFilmes.API.Models;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocadoraFilmes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SistemaController : Controller
    {
        private readonly ISistemaService _sistemaService;
        private readonly IUserService _userService;

        public SistemaController(ISistemaService sistemaService, IUserService userService)
        {
            _sistemaService = sistemaService;
            _userService = userService;
        }

        [HttpGet("VerificarPrimeiroUso")]
        public async Task<IActionResult> PrimeiroUso()
        {
            var existeUsuarioCadastrado = await _userService.ExisteUsuarioCadastrado();

            return Ok(new {PrimeiroUso = !existeUsuarioCadastrado});
        }

        [HttpGet("Dashboard")]
        //[Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var quantidadeItensDTO = await _sistemaService.GetQtdItens();
            return Ok(quantidadeItensDTO);
        }
    }
}
