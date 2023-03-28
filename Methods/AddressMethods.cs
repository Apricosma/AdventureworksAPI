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

    }
}
