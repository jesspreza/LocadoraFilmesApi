using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Interfaces
{
    public interface IFilmeRepository
    {
        Task<Filme> Create(Filme filme);
        Task<Filme> Update(Filme filme);
        Task<Filme> Delete(int id);
        Task<Filme> GetById(int id);
        Task<PagedList<Filme>> GetAll(int pageNumber, int pageSize);
        Task<Filme> GetForEdit(int id);
    }
}
