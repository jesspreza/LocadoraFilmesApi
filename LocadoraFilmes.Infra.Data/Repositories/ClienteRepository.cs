using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Interfaces;
using LocadoraFilmes.Infra.Data.Context;
using LocadoraFilmes.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraFilmes.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MSSQLContext _context;

        public ClienteRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Delete(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            return null;
        }

        public async Task<PagedList<Cliente>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Clientes.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _context.Clientes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
