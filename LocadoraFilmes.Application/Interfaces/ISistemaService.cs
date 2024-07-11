using LocadoraFilmes.Application.DTOs;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Interfaces
{
    public interface ISistemaService
    {
        Task<QuantidadeItensDTO> GetQtdItens();
    }
}
