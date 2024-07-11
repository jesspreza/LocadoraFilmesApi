using LocacaoFilmes.Domain.SystemModels;
using LocadoraFilmes.Domain.Interfaces;
using LocadoraFilmes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LocadoraFilmes.Infra.Data.Repositories
{
    public class SistemaRepository : ISistemaRepository
    {
        private readonly MSSQLContext _context;

        public SistemaRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<QuantidadeItens> GetQtdItens()
        {
            QuantidadeItens quantidadeItens = new QuantidadeItens();
            quantidadeItens.QtdCliente = await _context.Clientes.CountAsync();
            quantidadeItens.QtdGenero = await _context.Generos.CountAsync();
            quantidadeItens.QtdFilme = await _context.Filmes.CountAsync();
            quantidadeItens.QtdLocacao = await _context.Locacoes.CountAsync();

            return quantidadeItens;
        }
    }
}
