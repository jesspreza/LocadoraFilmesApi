using LocadoraFilmes.API.Extensions;
using LocadoraFilmes.API.Models;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Infra.Ioc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocadoraFilmes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : Controller
    {
        private readonly IGeneroService _generoService;
        private readonly IUserService _userService;

        public GeneroController(IGeneroService generoService, IUserService userService)
        {
            _generoService = generoService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(GeneroDTO generoDTO)
        {
            var generoDTOCadastrado = await _generoService.Create(generoDTO);
            if (generoDTOCadastrado == null)
                return BadRequest("Ocorreu um erro ao cadastrar o gênero");

            return Ok("Gênero cadastrado com sucesso!");
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(GeneroDTO generoDTO)
        {
            var generoDTOAlterado = await _generoService.Update(generoDTO);
            if (generoDTOAlterado == null)
                return BadRequest("Ocorreu um erro ao alterar o gênero");

            return Ok("Gênero alterado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var userId = User.GetId();
            var user = await _userService.GetById(userId);

            if (!user.IsAdmin)
                return Unauthorized("Você não tem permissão para excluir generos");

            var generoDTOExcluido = await _generoService.Delete(id);
            if (generoDTOExcluido == null)
                return BadRequest("Ocorreu um erro ao excluir o gênero");

            return Ok("Gênero excluído com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            var generoDTO = await _generoService.GetById(id);
            if (generoDTO == null)
                return NotFound("Gênero não encontrado");

            return Ok(generoDTO);
        }

        [HttpGet]
        public async Task<IActionResult> ListarGeneros([FromQuery] PaginationParams paginationParams)
        {
            var generosDTO = await _generoService.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(
                generosDTO.CurrentPage,
                generosDTO.PageSize,
                generosDTO.TotalCount,
                generosDTO.TotalPages
            ));

            return Ok(generosDTO);
        }
    }
}
