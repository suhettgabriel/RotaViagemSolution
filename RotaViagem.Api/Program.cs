using Microsoft.EntityFrameworkCore;
using RotaViagem.Infrastructure.Data;
using RotaViagem.Domain.Interfaces;
using RotaViagem.Infrastructure.Repositories;
using RotaViagem.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração da string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adicionando o DbContext ao container de serviços
builder.Services.AddDbContext<RotaViagemContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<RotaService>();

// Injeção de dependência para o repositório
builder.Services.AddScoped<IRotaRepository, RotaRepository>();

// Adicionando suporte a controladores
builder.Services.AddControllers();

// Configuração do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
