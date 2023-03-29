using AdventureworksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureworksAPI.Methods
{
    public class AddressMethods
    {
        public static IResult GetAddresses(AdventureWorksLt2019Context db)
        {
            return Results.Ok(db.Addresses);
        }

        public static IResult GetAddressById(AdventureWorksLt2019Context db, int id)
        {
            Address address = db.Addresses.FirstOrDefault(a => 
                a.AddressId == id);

            if (address == null)
            {
                return Results.NotFound(id);
            }

            return Results.Ok(address);
        }

        public static IResult CreateAddress(AdventureWorksLt2019Context db, Address address)
        {
            db.Addresses.Add(address);
            db.SaveChanges();

            return Results.Created($"/address/create", address);
        }

        public static IResult UpdateAddress(AdventureWorksLt2019Context db, int id, Address address)
        {
            Address selectedAddress = db.Addresses.Find(id);

            // if address is not found via Id create an address under that
            if (selectedAddress == null)
            {
                db.Addresses.Add(address);
                db.SaveChanges();

                return Results.Created($"/address/update", address);
            };

            selectedAddress.AddressLine1 = address.AddressLine1;
            selectedAddress.AddressLine2 = address.AddressLine2;
            selectedAddress.City = address.City;
            selectedAddress.StateProvince = address.StateProvince;
            selectedAddress.CountryRegion = address.CountryRegion;
            selectedAddress.PostalCode = address.PostalCode;
            selectedAddress.ModifiedDate = DateTime.Now;

            db.SaveChanges();

            return Results.Ok(selectedAddress);
        }

        public static IResult DeleteAddress(AdventureWorksLt2019Context db, int id)
        {
            Address address = db.Addresses.Find(id);

            if (address == null)
            {
                return Results.BadRequest($"Address of {id} was not found");
            }

            db.Addresses.Remove(address);
            db.SaveChanges();

            return Results.Ok($"Address of {id} was deleted successfully");
        }

        public static IResult AddressDetails(AdventureWorksLt2019Context db, int id)
        {
            List<Address> address = db.Addresses
            .Where(a => a.AddressId == id)
            .Include(a => a.CustomerAddresses)
                .ThenInclude(ca => ca.Customer)
            .ToList();

            if (address.Count == 0)
            {
                return Results.NotFound($"Address of {id} was not found");
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 0
            };

            // serialize and deserialize with reference handler
            // only solution I found to prevent a depth error
            string jsonResult = JsonSerializer.Serialize(address, options);
            Object jsonObject = JsonSerializer.Deserialize<object>(jsonResult);

            return Results.Ok(jsonObject);
        }

    }
}
