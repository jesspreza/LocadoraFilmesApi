using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO> Create(ClienteDTO clienteDTO);
        Task<ClienteDTO> Update(ClienteDTO clienteDTO);
        Task<ClienteDTO> Delete(int id);
        Task<ClienteDTO> GetById(int id);
        Task<PagedList<ClienteDTO>> GetAll(int pageNumber, int pageSize);
    }
}
