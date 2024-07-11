using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> Create(Cliente cliente);
        Task<Cliente> Update(Cliente cliente);
        Task<Cliente> Delete(int id);
        Task<Cliente> GetById(int id);
        Task<PagedList<Cliente>> GetAll(int pageNumber, int pageSize);
    }
}
