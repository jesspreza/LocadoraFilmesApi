using LocacaoFilmes.Domain.Account;
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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticate _authenticate;

        public UserController(IUserService userService, IAuthenticate authenticate)
        {
            _userService = userService;
            _authenticate = authenticate;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Cadastrar(UserDTO userDTO)
        {
            if(userDTO == null)
                return BadRequest("Dados inválidos");

            var existeEmail = await _authenticate.UserExists(userDTO.Email);

            if (existeEmail)
                return BadRequest("Este e-mail já está cadastrado.");

            var existeUsuarioCadastrado = await _userService.ExisteUsuarioCadastrado();
            if (!existeUsuarioCadastrado)
                userDTO.IsAdmin = true;
            else
            {
                if (User.FindFirst("id") == null)
                    return Unauthorized();

                var userId = User.GetId();
                var usuario = await _userService.GetById(userId);

                if (!usuario.IsAdmin)
                    return Unauthorized("Você não tem permissão para cadastrar novos usuários");
            }

            var user = await _userService.Create(userDTO);

            if (user == null)
                return BadRequest("Ocorreu um erro ao cadastrar");

            var token = _authenticate.GenerateToken(user.Id, user.Email);

            return new UserToken
            {
                Token = token
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Logar(LoginModel login)
        {
            var existeUsuario = await _authenticate.UserExists(login.Email);
            if (!existeUsuario)
                return Unauthorized("Usuário não existe");

            var result = await _authenticate.AuthenticateAsync(login.Email, login.Password);
            if (!result)
                return Unauthorized("Usuário ou senha inválido");

            var user = await _authenticate.GetUserByEmail(login.Email);

            var token = _authenticate.GenerateToken(user.Id, user.Email);

            return new UserToken { 
                Token = token,
                IsAdmin = user.IsAdmin,
                Email = user.Email,
                UserName = user.UserName
            };
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> Alterar(UserDTO userDTO)
        {
            var userIdLogado = User.GetId();
            var user = await _userService.GetById(userIdLogado);

            if (!user.IsAdmin && userDTO.Id != userIdLogado)
                return Unauthorized("Você não tem permissão para alterar os usuários do sistema");

            if(!user.IsAdmin && userDTO.Id == userIdLogado && userDTO.IsAdmin)
                return Unauthorized("Você não tem permissão para definir você mesmo como administrador");
            
            var userDTOAlterado = await _userService.Update(userDTO);
            if (userDTOAlterado == null)
                return BadRequest("Ocorreu um erro ao alterar o usuário");

            return Ok("usuário alterado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var userId = User.GetId();
            var user = await _userService.GetById(userId);

            if (!user.IsAdmin)
                return Unauthorized("Você não tem permissão para excluir usuários");

            var userDTOExcluido = await _userService.Delete(id);
            if (userDTOExcluido == null)
                return BadRequest("Ocorreu um erro ao excluir o usuário");

            return Ok("usuário excluído com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var userIdLogado = User.GetId();
            var userLogado = await _userService.GetById(userIdLogado);

            if (id == 0)
                id = userIdLogado;

            if (!userLogado.IsAdmin && userLogado.Id != id)
                return Unauthorized("Você não tem permissão para consultar os usuários do sistema");

            var userDTO = await _userService.GetById(id);
            if (userDTO == null)
                return NotFound("usuário não encontrado");

            return Ok(userDTO);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> ListarUsuarios([FromQuery] PaginationParams paginationParams)
        {
            var userIdLogado = User.GetId();
            var userLogado = await _userService.GetById(userIdLogado);

            if (!userLogado.IsAdmin)
                return Unauthorized("Você não tem permissão para consultar os usuários do sistema");

            var usersDTO = await _userService.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(
                usersDTO.CurrentPage, 
                usersDTO.PageSize,
                usersDTO.TotalCount,
                usersDTO.TotalPages
            ));

            return Ok(usersDTO);
        }
    }
}
