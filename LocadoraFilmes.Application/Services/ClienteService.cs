using AutoMapper;
using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<ClienteDTO> Create(ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            var clienteIncluido = await _clienteRepository.Create(cliente);
            return _mapper.Map<ClienteDTO>(clienteIncluido);
        }

        public async Task<ClienteDTO> Update(ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            var clienteAlterado = await _clienteRepository.Update(cliente);
            return _mapper.Map<ClienteDTO>(clienteAlterado);
        }

        public async Task<ClienteDTO> Delete(int id)
        {
            var clienteExcluido = await _clienteRepository.Delete(id);
            return _mapper.Map<ClienteDTO>(clienteExcluido);
        }

        public async Task<PagedList<ClienteDTO>> GetAll(int pageNumber, int pageSize)
        {
            var clientes = await _clienteRepository.GetAll(pageNumber, pageSize);
            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);

            return new PagedList<ClienteDTO>(clientesDTO, pageNumber, pageSize, clientes.TotalCount);
        }

        public async Task<ClienteDTO> GetById(int id)
        {
            var cliente = await _clienteRepository.GetById(id);
            return _mapper.Map<ClienteDTO>(cliente);
        }

    }
}
