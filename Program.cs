using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scaf;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>((serviceProvider, opts) =>
{
    opts.UseNpgsql(
        builder.Configuration.GetValue<string>("Db")
        );
});

var app = builder.Build();

app.MapGet("/", ([FromServices] MyDbContext ctx) =>
{
    ctx.Sellers.Add(new Seller()
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Bob"
    });
    ctx.SaveChanges();
    return ctx.Sellers.ToList();
});

app.Run();
