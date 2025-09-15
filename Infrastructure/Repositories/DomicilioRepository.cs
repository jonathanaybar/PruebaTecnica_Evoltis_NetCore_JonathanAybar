using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DomicilioRepository
        : RepositoryBase<Domicilio, Models.domicilio, AppDbContext>, IDomicilioRepository
    {
        public DomicilioRepository(AppDbContext ctx, IMapper mapper)
            : base(ctx, mapper)
        {
        }

        // Ejemplo: buscar por ciudad
        public async Task<IEnumerable<Domicilio>> BuscarPorCiudad(string ciudad, CancellationToken ct = default)
        {
            return await _ctx.domicilios
                .Where(d => d.Ciudad.Contains(ciudad))
                .ProjectTo<Domicilio>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }

        // Ejemplo: incluir usuario
        public async Task<Domicilio?> GetConUsuario(int id, CancellationToken ct = default)
        {
            var entity = await _ctx.domicilios
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(d => d.Id == id, ct);

            return _mapper.Map<Domicilio>(entity);
        }
    }
}

