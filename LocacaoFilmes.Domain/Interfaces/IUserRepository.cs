using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
        Task<User> GetById(int id);
        Task<PagedList<User>> GetAll(int pageNumber, int pageSize);
        Task<bool> ExisteUsuarioCadastrado();
    }
}
