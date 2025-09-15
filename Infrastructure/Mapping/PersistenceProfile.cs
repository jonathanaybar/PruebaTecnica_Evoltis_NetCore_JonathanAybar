using AutoMapper;
using Domain.Entities;
using Infrastructure.Models;

namespace Infrastructure.Mapping;

public class PersistenceProfile : Profile
{
    public PersistenceProfile()
    {
        #region Usuario
        CreateMap<Usuario, usuario>()
            .ForMember(d => d.domicilios, o => o.MapFrom(s => s.Domicilios));

        CreateMap<usuario, Usuario>()
            .ForMember(d => d.Domicilios, o => o.MapFrom(s => s.domicilios));
        #endregion

        #region Domicilio
        CreateMap<Domicilio, domicilio>()
            .ForMember(d => d.UsuarioID, o => o.MapFrom(s => s.UsuarioId));

        CreateMap<domicilio, Domicilio>()
            .ForMember(d => d.UsuarioId, o => o.MapFrom(s => s.UsuarioID))
            .ForMember(d => d.Usuario, o => o.Ignore()); // evita ciclos hacia Usuario
        #endregion
    }
}
