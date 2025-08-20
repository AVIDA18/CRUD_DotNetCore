using System;
using System.Collections.Generic;

namespace ProductsCRUD.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
