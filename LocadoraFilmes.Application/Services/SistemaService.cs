using AutoMapper;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Domain.Interfaces;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Services
{
    public class SistemaService : ISistemaService
    {
        private readonly ISistemaRepository _sistemaRepository;
        private readonly IMapper _mapper;

        public SistemaService(ISistemaRepository sistemaRepository, IMapper mapper)
        {
            _sistemaRepository = sistemaRepository;
            _mapper = mapper;
        }

        public async Task<QuantidadeItensDTO> GetQtdItens()
        {
            var quantidadeItens = await _sistemaRepository.GetQtdItens();
            var quantidadeItensDTO = _mapper.Map<QuantidadeItensDTO>(quantidadeItens);
            return quantidadeItensDTO;
        }
    }
}
