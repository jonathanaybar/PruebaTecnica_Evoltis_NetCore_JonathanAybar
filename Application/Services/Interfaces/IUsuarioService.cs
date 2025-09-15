using Application.DTOs;
using Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<UsuarioResponseDto>> GetAllAsync(CancellationToken ct = default);
        Task<UsuarioResponseDto> CreateAsync(UsuarioCreateRequestDto request, CancellationToken ct = default);
        Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioUpdateRequestDto request, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
