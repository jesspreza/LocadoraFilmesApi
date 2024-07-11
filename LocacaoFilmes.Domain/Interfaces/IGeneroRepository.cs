using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task<Genero> Create(Genero genero);
        Task<Genero> Update(Genero genero);
        Task<Genero> Delete(int id);
        Task<Genero> GetById(int id);
        Task<PagedList<Genero>> GetAll(int pageNumber, int pageSize);
    }
}
