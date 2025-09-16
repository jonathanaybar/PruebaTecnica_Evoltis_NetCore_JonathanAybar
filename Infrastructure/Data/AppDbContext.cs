//using Infrastructure.Models;
using Domain.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<domicilio> Domicilio { get; set; } = null!;
        public virtual DbSet<usuario> Usuario { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySql("server=localhost;port=3306;database=evoltispreubatecnica;user=root;treattinyasboolean=true", ServerVersion.Parse("10.4.28-mariadb"));
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
