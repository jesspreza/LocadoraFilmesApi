using LocadoraFilmes.API.Extensions;
using LocadoraFilmes.API.Models;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Infra.Ioc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LocadoraFilmes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocacaoController : Controller
    {
        private readonly ILocacaoService _locacaoService;
        private readonly IUserService _userService;

        public LocacaoController(ILocacaoService locacaoService, IUserService userService)
        {
            _locacaoService = locacaoService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(LocacaoPostDTO locacaoPostDTO)
        {
            foreach (var filmeId in locacaoPostDTO.FilmesId)
            {
                var filmeDisponivel = await _locacaoService.VerificarDisponibilidadeAsync(filmeId);
                if (!filmeDisponivel)
                    return BadRequest("Filme não disponível para locação!");
            }

            locacaoPostDTO.DataLocacao = DateTime.Now;
            locacaoPostDTO.Entregue = false;

            var locacaoDTOCadastrado = await _locacaoService.Create(locacaoPostDTO);
            if (locacaoDTOCadastrado == null)
                return BadRequest("Ocorreu um erro ao cadastrar a locação");

            return Ok("Locação cadastrada com sucesso!");
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(LocacaoPutDTO locacaoPutDTO)
        {
            var locacao = await _locacaoService.GetForEdit(locacaoPutDTO.Id);
            if (locacao == null)
                return NotFound("Locação não encontrada");

            locacao.Update(locacao.DataLocacao, locacaoPutDTO.DataDevolucao, locacaoPutDTO.Entregue);

            var locacaoDTOAlterado = await _locacaoService.Update(locacao);
            if (locacaoDTOAlterado == null)
                return BadRequest("Ocorreu um erro ao alterar a locação");

            return Ok("Locação alterado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var userId = User.GetId();
            var user = await _userService.GetById(userId);

            if (!user.IsAdmin)
                return Unauthorized("Você não tem permissão para excluir locações");

            var locacaoDTOExcluido = await _locacaoService.Delete(id);
            if (locacaoDTOExcluido == null)
                return BadRequest("Ocorreu um erro ao excluir a locação");

            return Ok("Locação excluída com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            var locacaoDTO = await _locacaoService.GetById(id);
            if (locacaoDTO == null)
                return NotFound("Locação não encontrada");

            return Ok(locacaoDTO);
        }

        [HttpGet]
        public async Task<IActionResult> ListarLocacaos([FromQuery] PaginationParams paginationParams)
        {
            var locacoesDTO = await _locacaoService.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(
                locacoesDTO.CurrentPage,
                locacoesDTO.PageSize,
                locacoesDTO.TotalCount,
                locacoesDTO.TotalPages
            ));

            return Ok(locacoesDTO);
        }
    }
}
