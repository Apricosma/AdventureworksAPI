using AdventureworksAPI.Methods;
using AdventureworksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLt2019Context")));

var app = builder.Build();

app.MapGet("/address", AddressMethods.GetAddresses);
app.MapGet("/address/{id:int}", AddressMethods.GetAddressById);
app.MapPost("/address/create", AddressMethods.CreateAddress);


app.Run();