using AdventureworksAPI.Methods;
using AdventureworksAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLt2019Context")));

var app = builder.Build();


app.MapPost("/Customer/create",CustomerMethods.CreateCustomer );
app.MapGet("/Customer/GetCustomerWithId", CustomerMethods.GetCustomerwithId);
app.MapGet("/Customer", CustomerMethods.GetAllCustomers);
app.MapDelete("/Customer/Delete", CustomerMethods.DeleteCustomer);
app.MapPut("Customer/Update", CustomerMethods.UpdateCustomer);

app.Run();