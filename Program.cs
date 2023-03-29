using AdventureworksAPI.Methods;
using AdventureworksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLt2019Context")));

var app = builder.Build();

// *** PRODUCT ENDPOINTS ***
app.MapGet("/product", ProductMethods.GetProduct);
app.MapGet("/product/{productId:int}", ProductMethods.GetProductById);
app.MapPost("/product/create", ProductMethods.Create);
app.MapPut("/product/update/{productId:int}", ProductMethods.Update);
app.MapDelete("/product/delete/{productId:int}", ProductMethods.Delete);

// *** SALES ORDER HEADER ENDPOINTS ***
app.MapGet("/salesorder", SalesOrderHeaderMethods.GetSalesOrder);
app.MapGet("/salesorder/{salesorderId:int}", SalesOrderHeaderMethods.GetSalesOrderById);
app.MapPost("/salesorder/create", SalesOrderHeaderMethods.Create);
app.MapPut("/salesorder/update/{salesorderId:int}", SalesOrderHeaderMethods.Update);
app.MapDelete("/salesorder/delete/{salesorderId:int}", SalesOrderHeaderMethods.Delete);

app.Run();