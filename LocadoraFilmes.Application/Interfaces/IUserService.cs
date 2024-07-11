using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO userDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task<UserDTO> Delete(int id);
        Task<UserDTO> GetById(int id);
        Task<PagedList<UserDTO>> GetAll(int pageNumber, int pageSize);
        Task<bool> ExisteUsuarioCadastrado();
    }
}
