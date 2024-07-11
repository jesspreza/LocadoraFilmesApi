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
    public class FilmeRepository : IFilmeRepository
    {
        private readonly MSSQLContext _context;

        public FilmeRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<Filme> Create(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme> Update(Filme filme)
        {
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme> Delete(int id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(x => x.Id == id);
            if (filme != null)
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();
                return filme;
            }
            return null;
        }

        public async Task<PagedList<Filme>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Filmes.Include(x => x.Genero).AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Filme> GetById(int id)
        {
            return await _context.Filmes.Include(x => x.Genero).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Filme> GetForEdit(int id)
        {
            return await _context.Filmes.FindAsync(id);
        }
    }
}
