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
    public class GeneroRepository : IGeneroRepository
    {
        private readonly MSSQLContext _context;

        public GeneroRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<Genero> Create(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
            return genero;
        }

        public async Task<Genero> Update(Genero genero)
        {
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
            return genero;
        }

        public async Task<Genero> Delete(int id)
        {
            var genero = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if (genero != null)
            {
                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();
                return genero;
            }
            return null;
        }

        public async Task<PagedList<Genero>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Generos.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Genero> GetById(int id)
        {
            return await _context.Generos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
