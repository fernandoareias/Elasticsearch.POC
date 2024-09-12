
using Elastic.Apm.NetCoreAll;
using Elasticsearch.POC.API.DTOs;
using Elasticsearch.POC.API.Middlewares;
using Elasticsearch.POC.API.Models.Interfaces;
using Elasticsearch.POC.API.Models.Interfaces.Services;
using Elasticsearch.POC.API.Serilog;
using Elasticsearch.POC.API.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAllElasticApm();
    builder.Services.AddSerilog();
    builder.Services.AddScoped<IClienteService, ClienteService>();
    builder.Services.AddScoped<ICartaoService, CartaoService>();
    
    builder.Services.AddScoped<IClienteRepository, IClienteRepository>();
    
    var app = builder.Build();
    app.UseCustomSerilog(builder.Configuration);
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseHttpsRedirection();
    app.UseMiddleware<CustomMiddleware>();

    
    app.MapPost("/login", (LoginRequest request) =>
        {
            bool sucesso = Random.Shared.NextDouble() >= 0.5;
    
            return new { sucesso };
        })
        .WithName("Login")
        .WithOpenApi();
    
    
    app.MapGet("/cartoes", () =>
        {
            var cartoes = Enumerable.Range(1, 3).Select(index =>
                    new
                    {
                        NomeCartao = $"Cartão {index}",
                        Numero = Random.Shared.Next(1000, 9999),
                        Saldo = Random.Shared.Next(0, 1000)
                    })
                .ToList();
        
            return cartoes;
        })
        .WithName("ListaCartoes")
        .WithOpenApi();
    
    
    app.MapPost("/compra", (CompraRequest request) =>
        {
            bool sucesso = Random.Shared.NextDouble() >= 0.5;
    
            return new { sucesso };
        })
        .WithName("Compra")
        .WithOpenApi();

    await app.RunAsync();
}
catch (Exception ex)
{
    
}