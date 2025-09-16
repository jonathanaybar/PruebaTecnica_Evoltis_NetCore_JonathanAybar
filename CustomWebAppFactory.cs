using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi; 
using Infrastructure.Data;

public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 1) Remover el DbContext real (MySQL)
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor is not null) services.Remove(descriptor);

            // 2) Crear conexión SQLite InMemory y dejarla abierta durante todo el test host
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // 3) Re-registrar el DbContext con SQLite
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlite(_connection);
            });

            // 4) Construir y asegurar la DB
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();

            // 5) (Opcional) Seed inicial
            Seed(db);
        });
    }

    private static void Seed(AppDbContext db)
    {
        // si querés datos iniciales:
        // db.Usuarios.Add(new Usuario { Nombre = "Seed", Email = "seed@example.com", FechaCreacion = DateTime.UtcNow });
        // db.SaveChanges();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}
