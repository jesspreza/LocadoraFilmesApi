using LocadoraFilmes.API.Extensions;
using LocadoraFilmes.API.Models;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Application.Services;
using LocadoraFilmes.Infra.Ioc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LocadoraFilmes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : Controller
    {
        private readonly IFilmeService _filmeService;
        private readonly IUserService _userService;

        public FilmeController(IFilmeService filmeService, IUserService userService)
        {
            _filmeService = filmeService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(FilmePostDTO filmeRequestDTO)
        {
            var filmeDTOCadastrado = await _filmeService.Create(filmeRequestDTO);
            if (filmeDTOCadastrado == null)
                return BadRequest("Ocorreu um erro ao cadastrar o filme");

            return Ok("Filme cadastrado com sucesso!");
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(FilmePutDTO filmePutDTO)
        {
            var filme = await _filmeService.GetForEdit(filmePutDTO.Id);
            if (filme == null)
                return NotFound("Locação não encontrada");

            filme.Update(filmePutDTO.Nome, filmePutDTO.Ativo, filmePutDTO.GeneroId);

            var filmeAlterado = await _filmeService.Update(filme);
            if (filmeAlterado == null)
                return BadRequest("Ocorreu um erro ao alterar o filme");

            return Ok("Filme alterado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var userId = User.GetId();
            var user = await _userService.GetById(userId);

            if (!user.IsAdmin)
                return Unauthorized("Você não tem permissão para excluir filmes");

            var filmeDTOExcluido = await _filmeService.Delete(id);
            if (filmeDTOExcluido == null)
                return BadRequest("Ocorreu um erro ao excluir o filme");

            return Ok("Filme excluído com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            var filmeDTO = await _filmeService.GetById(id);
            if (filmeDTO == null)
                return NotFound("Filme não encontrado");

            return Ok(filmeDTO);
        }

        [HttpGet]
        public async Task<IActionResult> ListarFilmes([FromQuery] PaginationParams paginationParams)
        {
            var filmesDTO = await _filmeService.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(
                filmesDTO.CurrentPage,
                filmesDTO.PageSize,
                filmesDTO.TotalCount,
                filmesDTO.TotalPages
            ));

            return Ok(filmesDTO);
        }
    }
}
