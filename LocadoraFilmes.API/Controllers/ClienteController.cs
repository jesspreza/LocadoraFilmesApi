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
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IUserService _userService;

        public ClienteController(IClienteService clienteService, IUserService userService)
        {
            _clienteService = clienteService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ClienteDTO clienteDTO)
        {
            var clienteDTOCadastrado = await _clienteService.Create(clienteDTO);
            if (clienteDTOCadastrado == null)
                return BadRequest("Ocorreu um erro ao cadastrar o cliente");

            return Ok("Cliente cadastrado com sucesso!");
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(ClienteDTO clienteDTO)
        {
            var clienteDTOAlterado = await _clienteService.Update(clienteDTO);
            if (clienteDTOAlterado == null)
                return BadRequest("Ocorreu um erro ao alterar o cliente");

            return Ok("Cliente alterado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var userId = User.GetId();
            var user = await _userService.GetById(userId);

            if (!user.IsAdmin)
                return Unauthorized("Você não tem permissão para excluir clientes");

            var clienteDTOExcluido = await _clienteService.Delete(id);
            if (clienteDTOExcluido == null)
                return BadRequest("Ocorreu um erro ao excluir o cliente");

            return Ok("Cliente excluído com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            var clienteDTO = await _clienteService.GetById(id);
            if (clienteDTO == null)
                return NotFound("Cliente não encontrado");

            return Ok(clienteDTO);
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes([FromQuery]PaginationParams paginationParams)
        {
            var clientesDTO = await _clienteService.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(
                clientesDTO.CurrentPage, 
                clientesDTO.PageSize, 
                clientesDTO.TotalCount, 
                clientesDTO.TotalPages
            ));

            return Ok(clientesDTO);
        }
    }
}
