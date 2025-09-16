using Infrastructure.Data; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 1) Quitar el DbContext real (MySQL)
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>)); // <-- AJUSTAR el DbContext
            if (descriptor is not null)
                services.Remove(descriptor);

            // 2) Conexión SQLite en memoria (persistente mientras corre el host)
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // 3) Registrar DbContext con SQLite
            services.AddDbContext<AppDbContext>(options =>   // <-- AJUSTAR el DbContext
            {
                options.UseSqlite(_connection);
            });

            // 4) Construir y crear schema
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // <-- AJUSTAR
            db.Database.EnsureCreated();

            // 5) (Opcional) Seed
            // Seed(db);
        });
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
