using AdventureworksAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

        public static IResult CreateAddress(AdventureWorksLt2019Context db
            , Address address)
        {
            db.Addresses.Add(address);
            db.SaveChanges();

            return Results.Created($"/address/create", address);
        }

    }
}
