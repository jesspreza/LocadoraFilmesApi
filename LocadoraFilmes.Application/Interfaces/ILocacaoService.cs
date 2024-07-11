using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Interfaces
{
    public interface ILocacaoService
    {
        Task<LocacaoDTO> Create(LocacaoPostDTO locacaoPostDTO);
        Task<Locacao> Update(Locacao locacao);
        Task<LocacaoDTO> Delete(int id);
        Task<LocacaoDTO> GetById(int id);
        Task<Locacao> GetForEdit(int id);
        Task<PagedList<LocacaoDTO>> GetAll(int pageNumber, int pageSize);
        Task<bool> VerificarDisponibilidadeAsync(int filmeId); 
    }
}
