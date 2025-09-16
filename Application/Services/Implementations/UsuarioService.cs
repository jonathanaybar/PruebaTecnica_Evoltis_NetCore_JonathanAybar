// Application/Services/UsuarioService.cs
using Application.DTOs;
using Application.DTOs.Usuario;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UsuarioResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var usuario = await _repo.Get(id, ct);
            return _mapper.Map<UsuarioResponseDto>(usuario);
        }

        public async Task<List<UsuarioResponseDto>> GetAllAsync(CancellationToken ct = default)
        {
            var usuarios = await _repo.GetAll(ct);
            return _mapper.Map<List<UsuarioResponseDto>>(usuarios);
        }

        public async Task<UsuarioResponseDto> CreateAsync(UsuarioCreateRequestDto request, CancellationToken ct = default)
        {
            var usuario = _mapper.Map<Usuario>(request);
            var created = await _repo.Add(usuario, ct);
            return _mapper.Map<UsuarioResponseDto>(created);
        }

        public async Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioUpdateRequestDto request, CancellationToken ct = default)
        {
            var user = await _repo.Get(id, ct);
            if (user is null) return null;

            user.Nombre = request.Nombre;
            user.Email = request.Email;

            if (request.Domicilios != null)
            {
                // Editar/agregar sin cambiar UsuarioId en existentes
                foreach (var d in request.Domicilios)
                {
                    if (d.Id == 0)
                    {
                        user.Domicilios.Add(new Domain.Entities.Domicilio
                        {
                            Calle = d.Calle,
                            Numero = d.Numero,
                            Ciudad = d.Ciudad,
                            Provincia = d.Provincia,
                            UsuarioId = user.Id
                        });
                    }
                    else
                    {
                        var existing = user.Domicilios.FirstOrDefault(x => x.Id == d.Id);
                        if (existing != null)
                        {
                            existing.Calle = d.Calle;
                            existing.Numero = d.Numero;
                            existing.Ciudad = d.Ciudad;
                            existing.Provincia = d.Provincia;
                        }
                    }
                }
            }

            await _repo.Update(user, ct);
            return _mapper.Map<UsuarioResponseDto>(user);
        }


        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var deleted = await _repo.Delete(id, ct);
            return deleted != null;
        }

        public async Task<List<UsuarioResponseDto>> SearchAsync(string? nombre, string? provincia, string? ciudad, CancellationToken ct = default)
        {
            var usuarios = await _repo.Search(
                string.IsNullOrWhiteSpace(nombre) ? null : nombre,
                string.IsNullOrWhiteSpace(provincia) ? null : provincia,
                string.IsNullOrWhiteSpace(ciudad) ? null : ciudad,
                ct);

            return _mapper.Map<List<UsuarioResponseDto>>(usuarios);
        }

    }
}

