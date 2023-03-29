using AdventureworksAPI.Models;

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

        public static IResult UpdateCustomer(AdventureWorksLt2019Context db, int id,Customer customer)
        {

            Customer UpdatedCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
            if(UpdatedCustomer == null)
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
            UpdatedCustomer.PasswordSalt= customer.PasswordSalt;
            UpdatedCustomer.PasswordHash = customer.PasswordHash;

                db.SaveChanges();
            return Results.Ok(UpdateCustomer);
        }

    }
}
