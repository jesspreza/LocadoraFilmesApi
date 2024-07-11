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
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IMapper _mapper;

        public LocacaoService(ILocacaoRepository locacaoRepository, IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
        }

        public async Task<LocacaoDTO> Create(LocacaoPostDTO locacaoPostDTO)
        {
            var locacao = _mapper.Map<Locacao>(locacaoPostDTO);
            var locacaoIncluido = await _locacaoRepository.Create(locacao);
            return _mapper.Map<LocacaoDTO>(locacaoIncluido);
        }

        public async Task<Locacao> Update(Locacao locacao)
        {
            return await _locacaoRepository.Update(locacao);
        }

        public async Task<LocacaoDTO> Delete(int id)
        {
            var locacaoExcluido = await _locacaoRepository.Delete(id);
            return _mapper.Map<LocacaoDTO>(locacaoExcluido);
        }

        public async Task<PagedList<LocacaoDTO>> GetAll(int pageNumber, int pageSize)
        {
            var locacoes = await _locacaoRepository.GetAll(pageNumber, pageSize);
            var locacoesDTO = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);

            return new PagedList<LocacaoDTO>(locacoesDTO, pageNumber, pageSize, locacoes.Count);
        }

        public async Task<LocacaoDTO> GetById(int id)
        {
            var locacao = await _locacaoRepository.GetById(id);
            return _mapper.Map<LocacaoDTO>(locacao);
        }

        public async Task<Locacao> GetForEdit(int id)
        {
            return await _locacaoRepository.GetForEdit(id);
        }

        public async Task<bool> VerificarDisponibilidadeAsync(int filmeId)
        {
            return await _locacaoRepository.VerificarDisponibilidade(filmeId);
        }
    }
}
