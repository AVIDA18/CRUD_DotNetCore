using System;
using System.Collections.Generic;

namespace ProductsCRUD.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public int ProductCategoryId { get; set; }

    public virtual ProductCategory ProductCategory { get; set; } = null!;
}
