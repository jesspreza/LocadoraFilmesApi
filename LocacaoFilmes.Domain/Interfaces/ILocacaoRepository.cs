using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<Locacao> Create(Locacao locacao);
        Task<Locacao> Update(Locacao locacao);
        Task<Locacao> Delete(int id);
        Task<Locacao> GetById(int id);
        Task<PagedList<Locacao>> GetAll(int pageNumber, int pageSize);
        Task<Locacao> GetForEdit(int id);
        Task<bool> VerificarDisponibilidade(int filmeId);
    }
}
