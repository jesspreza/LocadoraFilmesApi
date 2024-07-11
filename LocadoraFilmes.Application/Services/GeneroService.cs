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
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public GeneroService(IGeneroRepository generoRepository, IMapper mapper)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
        }

        public async Task<GeneroDTO> Create(GeneroDTO generoDTO)
        {
            var genero = _mapper.Map<Genero>(generoDTO);
            var generoIncluido = await _generoRepository.Create(genero);
            return _mapper.Map<GeneroDTO>(generoIncluido);
        }

        public async Task<GeneroDTO> Update(GeneroDTO generoDTO)
        {
            var genero = _mapper.Map<Genero>(generoDTO);
            var generoAlterado = await _generoRepository.Update(genero);
            return _mapper.Map<GeneroDTO>(generoAlterado);
        }

        public async Task<GeneroDTO> Delete(int id)
        {
            var generoExcluido = await _generoRepository.Delete(id);
            return _mapper.Map<GeneroDTO>(generoExcluido);
        }

        public async Task<PagedList<GeneroDTO>> GetAll(int pageNumber, int pageSize)
        {
            var generos = await _generoRepository.GetAll(pageNumber, pageSize);
            var generosDTO = _mapper.Map<IEnumerable<GeneroDTO>>(generos);

            return new PagedList<GeneroDTO>(generosDTO, pageNumber, pageSize, generos.Count);
        }

        public async Task<GeneroDTO> GetById(int id)
        {
            var genero = await _generoRepository.GetById(id);
            return _mapper.Map<GeneroDTO>(genero);
        }

    }
}
