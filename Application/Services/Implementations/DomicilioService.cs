using Application.DTOs.Domicilio;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class DomicilioService : IDomicilioService
    {
        private readonly IDomicilioRepository _repo;
        private readonly IMapper _mapper;

        public DomicilioService(IDomicilioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DomicilioResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var dom = await _repo.Get(id, ct);
            return _mapper.Map<DomicilioResponseDto>(dom);
        }

        public async Task<List<DomicilioResponseDto>> GetAllAsync(CancellationToken ct = default)
        {
            var domicilios = await _repo.GetAll(ct);
            return _mapper.Map<List<DomicilioResponseDto>>(domicilios);
        }

        public async Task<DomicilioResponseDto> CreateAsync(DomicilioCreateRequestDto request, CancellationToken ct = default)
        {
            var dom = _mapper.Map<Domicilio>(request);
            var created = await _repo.Add(dom, ct);
            return _mapper.Map<DomicilioResponseDto>(created);
        }

        public async Task<DomicilioResponseDto?> UpdateAsync(int id, DomicilioUpdateRequestDto request, CancellationToken ct = default)
        {
            var current = await _repo.Get(id, ct);
            if (current is null) return null;

            current.Calle = request.Calle;
            current.Numero = request.Numero;
            current.Ciudad = request.Ciudad;
            current.Provincia = request.Provincia;

            await _repo.Update(current, ct);

            return _mapper.Map<DomicilioResponseDto>(current);
        }


        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var deleted = await _repo.Delete(id, ct);
            return deleted != null;
        }
    }
}
