using AutoMapper;
using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Interfaces;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            if(userDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                byte[] passwordSalt = hmac.Key;

                user.AlterarSenha(passwordHash, passwordSalt);
            }

            var userIncluido = await _userRepository.Create(user);
            return _mapper.Map<UserDTO>(userIncluido);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var userAlterado = await _userRepository.Update(user);
            return _mapper.Map<UserDTO>(userAlterado);
        }

        public async Task<UserDTO> Delete(int id)
        {
            var userExcluido = await _userRepository.Delete(id);
            return _mapper.Map<UserDTO>(userExcluido);
        }

        public async Task<PagedList<UserDTO>> GetAll(int pageNumber, int pageSize)
        {
            var users = await _userRepository.GetAll(pageNumber, pageSize);
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);

            return new PagedList<UserDTO>(usersDTO, pageNumber, pageSize, users.TotalCount);
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> ExisteUsuarioCadastrado()
        {
            return await _userRepository.ExisteUsuarioCadastrado();
        }
    }
}
