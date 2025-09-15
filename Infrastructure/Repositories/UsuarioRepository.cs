using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository
        : RepositoryBase<Usuario, Models.usuario, AppDbContext>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext ctx, IMapper mapper)
            : base(ctx, mapper)
        {
        }

        // Ejemplo: buscar por nombre
        public async Task<IEnumerable<Usuario>> BuscarPorNombre(string nombre, CancellationToken ct = default)
        {
            return await _ctx.usuarios
                .Where(u => u.Nombre.Contains(nombre))
                .ProjectTo<Usuario>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }

        // Ejemplo: incluir domicilios
        public async Task<Usuario?> GetConDomicilios(int id, CancellationToken ct = default)
        {
            var entity = await _ctx.usuarios
                .Include(u => u.Domicilios)
                .FirstOrDefaultAsync(u => u.Id == id, ct);

            return _mapper.Map<Usuario>(entity);
        }
    }
}

