using Application.DTOs.Domicilio;
using Application.DTOs.Usuario;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
        #region Usuario
        //Response
        CreateMap<Usuario, UsuarioResponseDto>();
        //Requests
        CreateMap<UsuarioCreateRequestDto, Usuario>().ForMember(d => d.FechaCreacion, o => o.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UsuarioUpdateRequestDto, Usuario>();
        #endregion

        #region Domicilio
        //Response
        CreateMap<Domicilio, DomicilioResponseDto>();
        //Requests
        CreateMap<DomicilioCreateRequestDto, Domicilio>().ForMember(d => d.FechaCreacion, o => o.MapFrom(_ => DateTime.UtcNow)); ;
        CreateMap<DomicilioUpdateRequestDto, Domicilio>();
        #endregion
    }
}

