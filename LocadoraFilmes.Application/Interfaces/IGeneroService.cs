using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Interfaces
{
    public interface IGeneroService
    {
        Task<GeneroDTO> Create(GeneroDTO generoDTO);
        Task<GeneroDTO> Update(GeneroDTO generoDTO);
        Task<GeneroDTO> Delete(int id);
        Task<GeneroDTO> GetById(int id);
        Task<PagedList<GeneroDTO>> GetAll(int pageNumber, int pageSize);
    }
}
