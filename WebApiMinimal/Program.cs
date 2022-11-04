using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexts;
using WebApiMinimal.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(""));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("AddTotem", async (RequestIdTotem RequestIdTotem, Contexto contexto) =>
{
    RequestIdTotem.RequestIdBb = 0;
    RequestIdTotem.RequestIdArbi = "Totem0";
    contexto.RequestIdTotem.Add(RequestIdTotem);
    await contexto.SaveChangesAsync();
});

// app.MapPost("RemoveTotem/{id}", async (int id, Contexto contexto) =>
// {
//     var requestIdTotem = await contexto.RequestIdTotem.FirstOrDefaultAsync(p => p.Id == id);
//     if (requestIdTotem!= null)
//     {
//         contexto.RequestIdTotem.Remove(requestIdTotem);
//         await contexto.SaveChangesAsync();
//     }
// });

// app.MapGet("ListTotem", async (Contexto contexto) =>
// {
//    return await contexto.RequestIdTotem.ToListAsync();
// });

app.MapGet("GetTotem/{id}", async (int id,Contexto contexto) =>
{
   return await contexto.RequestIdTotem.FirstOrDefaultAsync(p=>p.Id ==id);
});
app.MapPut("UpdateArbi/{id}", async (int id,Contexto contexto) =>
{
    RequestIdTotem? ttm =  await contexto.RequestIdTotem.FirstOrDefaultAsync(p=>p.Id ==id);
    if(ttm!= null){
        if(!string.IsNullOrEmpty(ttm.RequestIdArbi)){
            string[] aux = ttm.RequestIdArbi.Split("Totem");
            int i = int.Parse(aux[1]);
            i++;
            ttm.RequestIdArbi = string.Concat(aux[0],i);
            await contexto.SaveChangesAsync();
        }
    }
});
app.MapPut("UpdateBb/{id}", async (int id,Contexto contexto) =>
{
    RequestIdTotem? ttm =  await contexto.RequestIdTotem.FirstOrDefaultAsync(p=>p.Id ==id);
    if(ttm!= null){
        ttm.RequestIdBb++;
        await contexto.SaveChangesAsync();
    }
});
app.UseSwaggerUI();

app.Run();
