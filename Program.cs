using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer(); // Preparando para o uso do API Swagger

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/dado/d{nroFaces}", (
    [FromRoute] int nroFaces
    ) =>
{
    if (nroFaces <= 0)
    {
        return Results.BadRequest(new { mensagem = "Somente dados com pelo menos uma face" });
    }

    return Results.Ok(new
    {
        dado = $"d{nroFaces}",
        rolagem = RandomNumberGenerator.GetInt32(1, nroFaces + 1)
    });
});


app.Run();