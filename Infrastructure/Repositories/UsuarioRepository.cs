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

        public async Task<List<Usuario>> Search(string? nombre, string? provincia, string? ciudad, CancellationToken ct = default)
        {
            var query = _ctx.Set<Infrastructure.Models.usuario>()
                           .Include(u => u.domicilios)
                           .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(u => EF.Functions.Like(u.Nombre!, $"%{nombre}%"));

            if (!string.IsNullOrWhiteSpace(provincia))
                query = query.Where(u => u.domicilios.Any(d => d.Provincia != null &&
                                                               EF.Functions.Like(d.Provincia, $"%{provincia}%")));

            if (!string.IsNullOrWhiteSpace(ciudad))
                query = query.Where(u => u.domicilios.Any(d => d.Ciudad != null &&
                                                               EF.Functions.Like(d.Ciudad, $"%{ciudad}%")));

            var models = await query.ToListAsync(ct);
            return _mapper.Map<List<Usuario>>(models);
        }
    }
}

