using AdventureworksAPI.Models;

namespace AdventureworksAPI.Methods
{
    public class SalesOrderHeaderMethods
    {
        public static IResult GetSalesOrder(AdventureWorksLt2019Context db)
        {
            return Results.Ok(db.SalesOrderHeaders);
        }


        public static IResult GetSalesOrderById(AdventureWorksLt2019Context db, int salesorderId)
        {
            SalesOrderHeader salesOrder = db.SalesOrderHeaders.FirstOrDefault(so =>
                so.SalesOrderId == salesorderId);

            if (salesOrder == null)
            {
                return Results.NotFound(salesorderId);
            }

            return Results.Ok(salesOrder);
        }


        public static IResult Create(AdventureWorksLt2019Context db, SalesOrderHeader salesOrder)
        {
            db.SalesOrderHeaders.Add(salesOrder);
            db.SaveChanges();

            return Results.Created($"/salesorder/create", salesOrder);
        }


        public static IResult Update(AdventureWorksLt2019Context db, int salesorderId, SalesOrderHeader salesOrder)
        {
            SalesOrderHeader selectedSalesOrder = db.SalesOrderHeaders.Find(salesorderId);

            // if address is not found via Id create an address under that
            if (selectedSalesOrder == null)
            {
                db.SalesOrderHeaders.Add(salesOrder);
                db.SaveChanges();

                return Results.Created($"/salesorder/update", salesOrder);
            };

            selectedSalesOrder.SalesOrderId = salesOrder.SalesOrderId;
            selectedSalesOrder.RevisionNumber = salesOrder.RevisionNumber;
            selectedSalesOrder.OrderDate = salesOrder.OrderDate;
            selectedSalesOrder.DueDate = salesOrder.DueDate;
            selectedSalesOrder.ShipDate = salesOrder.ShipDate;
            selectedSalesOrder.Status = salesOrder.Status;
            selectedSalesOrder.OnlineOrderFlag = salesOrder.OnlineOrderFlag;
            selectedSalesOrder.SalesOrderNumber = salesOrder.SalesOrderNumber;
            selectedSalesOrder.PurchaseOrderNumber = salesOrder.PurchaseOrderNumber;
            selectedSalesOrder.AccountNumber = salesOrder.AccountNumber;
            selectedSalesOrder.CustomerId = salesOrder.CustomerId;
            selectedSalesOrder.ShipToAddressId = salesOrder.ShipToAddressId;
            selectedSalesOrder.BillToAddressId = salesOrder.BillToAddressId;
            selectedSalesOrder.ShipMethod = salesOrder.ShipMethod;
            selectedSalesOrder.CreditCardApprovalCode = salesOrder.CreditCardApprovalCode;
            selectedSalesOrder.SubTotal = salesOrder.SubTotal;
            selectedSalesOrder.TaxAmt = salesOrder.TaxAmt;
            selectedSalesOrder.Freight = salesOrder.Freight;
            selectedSalesOrder.TotalDue = salesOrder.TotalDue;
            selectedSalesOrder.Comment = salesOrder.Comment;
            selectedSalesOrder.Rowguid = salesOrder.Rowguid;
            selectedSalesOrder.ModifiedDate = salesOrder.ModifiedDate;
            selectedSalesOrder.BillToAddress = salesOrder.BillToAddress;
            selectedSalesOrder.Customer = salesOrder.Customer;
            selectedSalesOrder.ShipToAddress = salesOrder.ShipToAddress;

            db.SaveChanges();

            return Results.Ok(selectedSalesOrder);
        }

        public static IResult Delete(AdventureWorksLt2019Context db, int salesorderId)
        {
            SalesOrderHeader salesOrder = db.SalesOrderHeaders.Find(salesorderId);

            if (salesOrder == null)
            {
                return Results.BadRequest($"Sales Order number {salesorderId} was not found");
            }

            db.SalesOrderHeaders.Remove(salesOrder);
            db.SaveChanges();

            return Results.Ok($"Sales Order number {salesorderId} was deleted successfully");
        }
    }
}
