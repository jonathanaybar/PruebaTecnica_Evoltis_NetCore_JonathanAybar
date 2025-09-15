using Application.DTOs.Domicilio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IDomicilioService
    {
        Task<DomicilioResponseDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<DomicilioResponseDto>> GetAllAsync(CancellationToken ct = default);
        Task<DomicilioResponseDto> CreateAsync(DomicilioCreateRequestDto request, CancellationToken ct = default);
        Task<DomicilioResponseDto?> UpdateAsync(int id, DomicilioUpdateRequestDto request, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
