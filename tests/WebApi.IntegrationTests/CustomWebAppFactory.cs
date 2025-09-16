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
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // 1) Quitar cualquier registro previo del contexto real
            var toRemove = services.Where(d =>
                    d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                    d.ServiceType == typeof(AppDbContext))
                .ToList();
            foreach (var d in toRemove) services.Remove(d);

            // 2) Usar UNA conexión en memoria compartida y mantenerla abierta
            _connection = new SqliteConnection("DataSource=:memory:;Cache=Shared");
            _connection.Open();

            services.AddDbContext<AppDbContext>(opts => opts.UseSqlite(_connection));

            // 3) Crear el esquema desde cero (evita “table already exists”)
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection?.Dispose();
    }
}
