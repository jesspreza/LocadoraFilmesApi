using LocadoraFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraFilmes.Infra.Data.EntitiesConfiguration
{
    //Classe para configurar convenções das diferentes entidades no banco de dados
    public class LocacaoFilmeConfiguration : IEntityTypeConfiguration<LocacaoFilme>
    {
        public void Configure(EntityTypeBuilder<LocacaoFilme> builder)
        {
            
        }
    }
}
