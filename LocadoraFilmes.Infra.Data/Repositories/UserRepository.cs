using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Interfaces;
using LocadoraFilmes.Infra.Data.Context;
using LocadoraFilmes.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraFilmes.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MSSQLContext _context;

        public UserRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<PagedList<User>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Users.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteUsuarioCadastrado()
        {
            return await _context.Users.AnyAsync();
        }
    }
}
