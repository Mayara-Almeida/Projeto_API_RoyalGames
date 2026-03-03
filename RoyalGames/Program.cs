using Microsoft.EntityFrameworkCore;
using RoyalGames.Applications.Services;
using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Repositores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexão com banco
builder.Services.AddDbContext<RoyalGamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Usuário
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
