using System;
using System.Collections.Generic;

namespace AdventureworksAPI.Models;

public partial class ProductModel
{
    public int ProductModelId { get; set; }

    public string Name { get; set; } = null!;

    public string? CatalogDescription { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; } = new List<ProductModelProductDescription>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
