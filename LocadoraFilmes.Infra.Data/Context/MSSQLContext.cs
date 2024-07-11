using LocadoraFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocadoraFilmes.Infra.Data.Context
{
    public class MSSQLContext :DbContext
    {
        public MSSQLContext() { }

        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options) { }

        // Define as propriedades DbSet para cada entidade
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<LocacaoFilme> LocacaoFilmes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(MSSQLContext).Assembly);
        }
    }
}
