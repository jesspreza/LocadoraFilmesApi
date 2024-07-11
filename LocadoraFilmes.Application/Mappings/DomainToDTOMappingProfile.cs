using AutoMapper;
using LocacaoFilmes.Domain.SystemModels;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Domain.Entities;

namespace LocadoraFilmes.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<FilmeDTO, Filme>().ReverseMap()
                .ForMember(dest => dest.GeneroDTO, opt => opt.MapFrom(x => x.Genero));
            CreateMap<Filme, FilmePostDTO>().ReverseMap();
            CreateMap<FilmePutDTO, Filme>().ReverseMap();
            CreateMap<LocacaoDTO, Locacao>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.ClienteDTO, opt => opt.MapFrom(x => x.Cliente))
                .ForMember(dest => dest.LocacaoFilmesDTO, opt => opt.MapFrom(x => x.LocacaoFilmes));
            CreateMap<LocacaoPostDTO, Locacao>().ReverseMap();
            CreateMap<LocacaoPutDTO, Locacao>().ReverseMap();
            CreateMap<LocacaoFilmeDTO, LocacaoFilme>().ReverseMap()
                .ForMember(dest => dest.Locacao, opt => opt.Ignore())
                .ForMember(dest => dest.LocacaoId, opt => opt.Ignore())
                .ForMember(dest => dest.FilmeId, opt => opt.Ignore())
                .ForMember(dest => dest.FilmeDTO, opt => opt.MapFrom(x => x.Filme));
            CreateMap<QuantidadeItens, QuantidadeItensDTO>().ReverseMap();
        }
    }
}
