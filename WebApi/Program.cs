using Application;
using Application.Mapping;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Mapping;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Controllers + JSON
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));

// Inyecciones de dependencia
builder.Services.AddInfrastructure();
builder.Services.AddApplicationServices();

//Validators
builder.Services.AddApiValidations();

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(DtoProfile).Assembly,
    typeof(PersistenceProfile).Assembly
);


var app = builder.Build();

//Automigrar al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
