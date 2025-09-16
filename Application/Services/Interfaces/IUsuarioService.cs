using Application.DTOs;
using Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUsuarioService : IServiceBase<UsuarioCreateRequestDto, UsuarioUpdateRequestDto, UsuarioResponseDto, int>
    {
        Task<List<UsuarioResponseDto>> SearchAsync(string? nombre, string? provincia, string? ciudad, CancellationToken ct = default);
    }
}
