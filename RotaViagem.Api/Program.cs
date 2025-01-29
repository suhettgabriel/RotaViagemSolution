using Microsoft.EntityFrameworkCore;
using RotaViagem.Infrastructure.Data;
using RotaViagem.Domain.Interfaces;
using RotaViagem.Infrastructure.Repositories;
using RotaViagem.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura��o da string de conex�o
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adicionando o DbContext ao container de servi�os
builder.Services.AddDbContext<RotaViagemContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<RotaService>();

// Inje��o de depend�ncia para o reposit�rio
builder.Services.AddScoped<IRotaRepository, RotaRepository>();

// Adicionando suporte a controladores
builder.Services.AddControllers();

// Configura��o do Swagger para documenta��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura��o do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
