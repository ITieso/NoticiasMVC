using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Domain.Entities;
using AutoMapper;

namespace ArchitectureMvc.Application.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Noticia, NoticiaDto>().ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.Usuario.Id))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.NoticiaTags.Select(nt => nt.Tag.Nome).ToList())).ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
