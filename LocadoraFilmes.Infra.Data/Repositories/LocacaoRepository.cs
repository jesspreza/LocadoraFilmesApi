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
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly MSSQLContext _context;

        public LocacaoRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<Locacao> Create(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();

            foreach (var filmeId in locacao.FilmesId)
            {
                var locacaoFilme = new LocacaoFilme { 
                    FilmeId = filmeId,
                    LocacaoId = locacao.Id
                };
                _context.LocacaoFilmes.Add(locacaoFilme);
            }

            await _context.SaveChangesAsync();

            return locacao;
        }

        public async Task<Locacao> Update(Locacao locacao)
        {
            _context.Locacoes.Update(locacao);
            await _context.SaveChangesAsync();
            return locacao;
        }

        public async Task<Locacao> Delete(int id)
        {
            var locacao = await _context.Locacoes.FirstOrDefaultAsync(x => x.Id == id);
            if (locacao != null)
            {
                _context.Locacoes.Remove(locacao);
                await _context.SaveChangesAsync();
                return locacao;
            }
            return null;
        }

        public async Task<PagedList<Locacao>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Locacoes
                .Include(x => x.LocacaoFilmes)
                    .ThenInclude(lf => lf.Filme)
                    .ThenInclude(f => f.Genero)
                .Include(x => x.Cliente).AsQueryable();

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Locacao> GetById(int id)
        {
            return await _context.Locacoes
                .Include(x => x.LocacaoFilmes)
                    .ThenInclude(lf => lf.Filme)
                .Include(x => x.Cliente).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Locacao> GetForEdit(int id)
        {
            return await _context.Locacoes.FindAsync(id);
        }

        public async Task<bool> VerificarDisponibilidade(int filmeId)
        {
            var existeLocacao = await _context.LocacaoFilmes
                .Where(x => x.FilmeId == filmeId && x.Locacao.Entregue == false).AnyAsync();

            return !existeLocacao;
        }
    }
}
