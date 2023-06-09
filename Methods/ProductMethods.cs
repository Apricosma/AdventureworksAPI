﻿using AdventureworksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AdventureworksAPI.Methods
{
    public class ProductMethods
    {
        public static IResult GetProduct(AdventureWorksLt2019Context db)
        {
            return Results.Ok(db.Products);
        }
    
       

        public static IResult GetProductById(AdventureWorksLt2019Context db, int productId)
        {
            Product product = db.Products.FirstOrDefault(p =>
                p.ProductId == productId);

            if (product == null)
            {
                return Results.NotFound(productId);
            }

            return Results.Ok(product);
        }


        public static IResult Create(AdventureWorksLt2019Context db, Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();

            return Results.Created($"/product/create", product);
        }

        public static IResult Update(AdventureWorksLt2019Context db, int productId, Product product)
        {
            Product selectedProduct = db.Products.Find(productId);

            // if address is not found via Id create an address under that
            if (selectedProduct == null)
            {
                db.Products.Add(product);
                db.SaveChanges();

                return Results.Created($"/product/update", product);
            };

            selectedProduct.Name = product.Name;
            selectedProduct.ProductNumber = product.ProductNumber;
            selectedProduct.Color = product.Color;
            selectedProduct.StandardCost = product.StandardCost;
            selectedProduct.ListPrice = product.ListPrice;
            selectedProduct.Size = product.Size;
            selectedProduct.Weight = product.Weight;
            selectedProduct.ProductCategoryId = product.ProductCategoryId;
            selectedProduct.ProductModelId = product.ProductModelId;
            selectedProduct.SellStartDate = product.SellStartDate;
            selectedProduct.SellEndDate = product.SellEndDate;
            selectedProduct.DiscontinuedDate = product.DiscontinuedDate;
            selectedProduct.ThumbNailPhoto = product.ThumbNailPhoto;
            selectedProduct.ThumbnailPhotoFileName = product.ThumbnailPhotoFileName;
            selectedProduct.ModifiedDate = DateTime.Now;
            selectedProduct.ProductCategory = product.ProductCategory;
            selectedProduct.ProductModel = product.ProductModel;

            db.SaveChanges();

            return Results.Ok(selectedProduct);
        }

        public static IResult Delete(AdventureWorksLt2019Context db, int productId)
        {
            Product product = db.Products.Find(productId);

            if (product == null)
            {
                return Results.BadRequest($"Product {product.Name} was not found");
            }

            List<SalesOrderDetail> details = db.SalesOrderDetails
                .Where(sod => sod.ProductId == productId)
                .ToList();

            db.SalesOrderDetails.RemoveRange(details);
            db.Products.Remove(product);
            db.SaveChanges();

            return Results.Ok($"Product {product.Name} was deleted successfully");
        }

        public static IResult Details(AdventureWorksLt2019Context db, int productId) 
        {
            List<Product> products = db.Products
                .Where(p => p.ProductId ==  productId)
                .Include(pc => pc.ProductCategory)
                .Include(m => m.ProductModel)
                .ThenInclude(pm => pm.ProductModelProductDescriptions)
                .ThenInclude(c => c.ProductDescription)
                .ToList();

            if (products.Count == 0)
            {
                return Results.NotFound($"Product with ID {productId} not found");
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 0
            };

            string jsonResult = JsonSerializer.Serialize(products, options);
            Object jsonObject = JsonSerializer.Deserialize<object>(jsonResult);

            return Results.Ok(jsonObject);
        }

    }
}
