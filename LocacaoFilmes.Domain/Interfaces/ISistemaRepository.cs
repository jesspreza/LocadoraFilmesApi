using LocacaoFilmes.Domain.SystemModels;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Interfaces
{
    public interface ISistemaRepository
    {
        Task<QuantidadeItens> GetQtdItens();
    }
}
