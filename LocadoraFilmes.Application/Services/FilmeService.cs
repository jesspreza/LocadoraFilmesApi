using AutoMapper;
using LocacaoFilmes.Domain.Pagination;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IMapper _mapper;

        public FilmeService(IFilmeRepository filmeRepository, IMapper mapper)
        {
            _filmeRepository = filmeRepository;
            _mapper = mapper;
        }

        public async Task<FilmeDTO> Create(FilmePostDTO filmeRequestDTO)
        {
            filmeRequestDTO.DataCriacao = DateTime.Now;
            filmeRequestDTO.Ativo = true;

            var filme = _mapper.Map<Filme>(filmeRequestDTO);
            var filmeIncluido = await _filmeRepository.Create(filme);
            return _mapper.Map<FilmeDTO>(filmeIncluido);
        }

        public async Task<Filme> Update(Filme filme)
        {
            return await _filmeRepository.Update(filme);
        }

        public async Task<FilmeDTO> Delete(int id)
        {
            var filmeExcluido = await _filmeRepository.Delete(id);
            return _mapper.Map<FilmeDTO>(filmeExcluido);
        }

        public async Task<PagedList<FilmeDTO>> GetAll(int pageNumber, int pageSize)
        {
            var filmes = await _filmeRepository.GetAll(pageNumber, pageSize);
            var filmesDTO = _mapper.Map<IEnumerable<FilmeDTO>>(filmes);

            return new PagedList<FilmeDTO>(filmesDTO, pageNumber, pageSize, filmes.Count);
        }

        public async Task<FilmeDTO> GetById(int id)
        {
            var filme = await _filmeRepository.GetById(id);
            return _mapper.Map<FilmeDTO>(filme);
        }

        public async Task<Filme> GetForEdit(int id)
        {
            return await _filmeRepository.GetForEdit(id);
        }

    }
}
