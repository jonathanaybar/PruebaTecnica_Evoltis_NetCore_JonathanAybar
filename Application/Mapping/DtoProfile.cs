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
        CreateMap<UsuarioCreateRequestDto, Usuario>();
        CreateMap<UsuarioUpdateRequestDto, Usuario>();
        #endregion

        #region Domicilio
        //Response
        CreateMap<Domicilio, DomicilioResponseDto>();
        //Requests
        CreateMap<DomicilioCreateRequestDto, Domicilio>();
        CreateMap<DomicilioUpdateRequestDto, Domicilio>();
        #endregion
    }
}

