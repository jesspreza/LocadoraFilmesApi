using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Interfaces
{
    public interface IFilmeService
    {
        Task<FilmeDTO> Create(FilmePostDTO filmeRequestDTO);
        Task<Filme> Update(Filme filme);
        Task<FilmeDTO> Delete(int id);
        Task<FilmeDTO> GetById(int id);
        Task<PagedList<FilmeDTO>> GetAll(int pageNumber, int pageSize);
        Task<Filme> GetForEdit(int id);
    }
}
