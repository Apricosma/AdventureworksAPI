using AdventureworksAPI.Migrations;
using AdventureworksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;



namespace AdventureworksAPI.Methods
{
    public static class CustomerMethods
    {
        public static IResult GetCustomerwithId(AdventureWorksLt2019Context db, int id)
        {
            Customer customer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(customer);
        }

        public static IResult GetAllCustomers(AdventureWorksLt2019Context db)
        {
            return Results.Ok(new
            {
                Customer = db.Customers.ToList()
            });




        }

        public static IResult CreateCustomer(AdventureWorksLt2019Context db, Customer customer)
        {
            db.Add(customer);
            db.SaveChanges();
            return Results.Ok();
        }

        public static IResult DeleteCustomer(AdventureWorksLt2019Context db, int id)
        {
            Customer customer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return Results.Ok();

        }

        public static IResult UpdateCustomer(AdventureWorksLt2019Context db, int id, Customer customer)
        {

            Customer UpdatedCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (UpdatedCustomer == null)
            {

                db.Add(customer);
                db.SaveChanges();
            }
            UpdatedCustomer.CustomerId = customer.CustomerId;
            UpdatedCustomer.EmailAddress = customer.EmailAddress;
            UpdatedCustomer.FirstName = customer.FirstName;
            UpdatedCustomer.LastName = customer.LastName;
            UpdatedCustomer.Title = customer.Title;
            UpdatedCustomer.MiddleName = customer.MiddleName;
            UpdatedCustomer.NameStyle = customer.NameStyle;
            UpdatedCustomer.CompanyName = customer.CompanyName;
            UpdatedCustomer.ModifiedDate = customer.ModifiedDate;
            UpdatedCustomer.Rowguid = customer.Rowguid;
            UpdatedCustomer.Phone = customer.Phone;
            UpdatedCustomer.Suffix = customer.Suffix;
            UpdatedCustomer.SalesPerson = customer.SalesPerson;
            UpdatedCustomer.PasswordSalt = customer.PasswordSalt;
            UpdatedCustomer.PasswordHash = customer.PasswordHash;

            db.SaveChanges();
            return Results.Ok(UpdateCustomer);
        }


        public static IResult CustomerDetails(AdventureWorksLt2019Context db, int id)
        {
            List<Customer> customer = db.Customers.Where(c => c.CustomerId == id)
                .Include(c => c.CustomerAddresses)
                .ThenInclude(c => c.Address)
                .ToList();

            if (customer.Count == 0)
            {
                return Results.NotFound($"Custmer with {id} is not registered");
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 0
            };

        
            string jsonResult = JsonSerializer.Serialize(customer, options);
            Object jsonObject = JsonSerializer.Deserialize<object>(jsonResult);

            return Results.Ok(jsonObject);


        }

        public static IResult AddCustomerToAddress(AdventureWorksLt2019Context db, JsonElement json)
        {
            int customerId = json.GetProperty("customerId").GetInt32();
            int addressId = json.GetProperty("addressId").GetInt32();

            Customer selectedCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            Address address = db.Addresses.FirstOrDefault(a => a.AddressId == addressId);

            if (selectedCustomer == null)
            {
                return Results.NotFound($"Customer of {customerId} was not found");
            }

            if (address == null) 
            {
                return Results.NotFound($"Address of {addressId} was not found");
            }

            if (db.CustomerAddresses.Any(ca => ca.CustomerId == customerId && ca.AddressId == addressId))
            {
                return Results.BadRequest($"ERROR: customerId: {customerId} already on address {addressId}");
            }

            CustomerAddress customerAddress = new CustomerAddress();
            customerAddress.CustomerId = selectedCustomer.CustomerId;
            customerAddress.AddressId = address.AddressId;
            customerAddress.AddressType = "Main Office";
            customerAddress.Rowguid = Guid.NewGuid();
            customerAddress.ModifiedDate = DateTime.Now;

            db.CustomerAddresses.Add(customerAddress);
            db.SaveChanges();

            return Results.Ok($"Customer {selectedCustomer.FirstName} added to address {address.AddressLine1}");
        }
    }
}
