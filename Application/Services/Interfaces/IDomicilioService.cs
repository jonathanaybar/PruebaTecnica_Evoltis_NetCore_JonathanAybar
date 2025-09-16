using Application.DTOs.Domicilio;
using Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IDomicilioService : IServiceBase<DomicilioCreateRequestDto, DomicilioUpdateRequestDto, DomicilioResponseDto, int>

    {
    }
}
