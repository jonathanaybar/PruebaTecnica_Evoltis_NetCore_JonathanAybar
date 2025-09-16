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
    }
}

