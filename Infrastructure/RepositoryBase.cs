using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Infrastructure
{
    public abstract class RepositoryBase<TDomain, TModel, TContext> : IRepository<TDomain>
        where TDomain : class, IEntity
        where TModel : class
        where TContext : DbContext
    {
        protected readonly TContext _ctx;
        protected readonly IMapper _mapper;

        protected RepositoryBase(TContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        protected DbSet<TModel> Set() => _ctx.Set<TModel>();

        public virtual async Task<TDomain?> Get(int id, CancellationToken ct = default)
        {
            return await Set()
                .ProjectTo<TDomain>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, ct);
        }

        public virtual async Task<List<TDomain>> GetAll(CancellationToken ct = default)
        {
            return await Set()
                .ProjectTo<TDomain>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }

        public virtual async Task<TDomain> Add(TDomain domain, CancellationToken ct = default)
        {
            var model = _mapper.Map<TModel>(domain);
            await Set().AddAsync(model, ct);
            await _ctx.SaveChangesAsync(ct);
            return _mapper.Map<TDomain>(model);
        }

        public virtual async Task<TDomain?> Delete(int id, CancellationToken ct = default)
        {
            var model = await Set().FindAsync(new object[] { id }, ct);
            if (model is null) return null;

            Set().Remove(model);
            await _ctx.SaveChangesAsync(ct);
            return _mapper.Map<TDomain>(model);
        }

        public virtual async Task<TDomain> Update(TDomain domain, CancellationToken ct = default)
        {
            var model = await Set().FindAsync(new object[] { domain.Id }, ct);
            if (model is null)
                throw new KeyNotFoundException($"Id {domain.Id} no existe.");

            _mapper.Map(domain, model); // aplica cambios sobre la entidad existente
            await _ctx.SaveChangesAsync(ct);
            return _mapper.Map<TDomain>(model);
        }
    }
}