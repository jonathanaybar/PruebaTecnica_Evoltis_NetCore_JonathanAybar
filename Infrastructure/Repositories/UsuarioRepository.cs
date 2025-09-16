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
            var q = _ctx.Set<Usuario>()                 
                .Include(u => u.Domicilios)
                .AsNoTracking()                         
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                q = q.Where(u => u.Nombre != null && EF.Functions.Like(u.Nombre, $"%{nombre}%"));

            if (!string.IsNullOrWhiteSpace(provincia))
                q = q.Where(u => u.Domicilios.Any(d => d.Provincia == provincia));

            if (!string.IsNullOrWhiteSpace(ciudad))
                q = q.Where(u => u.Domicilios.Any(d => d.Ciudad == ciudad));

            return await q.OrderBy(u => u.Nombre).ToListAsync(ct); 
        }


    }
}

