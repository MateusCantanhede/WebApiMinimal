using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexts;
using WebApiMinimal.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer("Data Source=tcp:webapiminimaldbserver.database.windows.net,1433;Initial Catalog=WebApiMinimal_db;User Id=mateus@webapiminimaldbserver;Password=Suco#7913"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("AdicionaProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
});

app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produtoExcluir = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
    if (produtoExcluir!= null)
    {
        contexto.Produto.Remove(produtoExcluir);
        await contexto.SaveChangesAsync();
    }
});

app.MapGet("ListarProdutos", async (Contexto contexto) =>
{
   return await contexto.Produto.ToListAsync();
});

app.MapGet("ObterProduto/{id}", async (int id,Contexto contexto) =>
{
   return await contexto.Produto.FirstOrDefaultAsync(p=>p.Id ==id);
});
app.MapGet("/", async (Contexto contexto) =>
{
    return await contexto.Produto.ToListAsync();
});
app.UseSwaggerUI();

app.Run();
