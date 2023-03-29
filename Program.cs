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

// *** PRODUCT ENDPOINTS ***
app.MapGet("/product", ProductMethods.GetProduct);
app.MapGet("/product/{productId:int}", ProductMethods.GetProductById);
app.MapPost("/product/create", ProductMethods.Create);
app.MapPut("/product/update/{productId:int}", ProductMethods.Update);
app.MapDelete("/product/delete/{productId:int}", ProductMethods.Delete);
app.MapGet("/product/details/{productId:int}", ProductMethods.Details);

// *** SALES ORDER HEADER ENDPOINTS ***
app.MapGet("/salesorder", SalesOrderHeaderMethods.GetSalesOrder);
app.MapGet("/salesorder/{salesorderId:int}", SalesOrderHeaderMethods.GetSalesOrderById);
app.MapPost("/salesorder/create", SalesOrderHeaderMethods.Create);
app.MapPut("/salesorder/update/{salesorderId:int}", SalesOrderHeaderMethods.Update);
app.MapDelete("/salesorder/delete/{salesorderId:int}", SalesOrderHeaderMethods.Delete);

//***Customer Endpoints***
app.MapPost("/Customer/create",CustomerMethods.CreateCustomer );
app.MapGet("/Customer/GetCustomerWithId", CustomerMethods.GetCustomerwithId);
app.MapGet("/Customer", CustomerMethods.GetAllCustomers);
app.MapDelete("/Customer/Delete", CustomerMethods.DeleteCustomer);
app.MapPut("Customer/Update", CustomerMethods.UpdateCustomer);
app.MapGet("Customer/Details", CustomerMethods.CustomerDetails);
app.MapPost("Customer/AddtoAddress", CustomerMethods.AddingCustomer);
app.MapPost("/Customer/AddtoAddress", CustomerMethods.AddCustomerToAddress);

// *** ADDRESS ENDPOINTS ***
app.MapGet("/address", AddressMethods.GetAddresses);
app.MapGet("/address/{id:int}", AddressMethods.GetAddressById);
app.MapPost("/address/create", AddressMethods.CreateAddress);
app.MapPut("/address/update/{id:int}", AddressMethods.UpdateAddress);
app.MapDelete("/address/delete/{id:int}", AddressMethods.DeleteAddress);
app.MapGet("/address/details/{id:int}", AddressMethods.AddressDetails);

app.Run();