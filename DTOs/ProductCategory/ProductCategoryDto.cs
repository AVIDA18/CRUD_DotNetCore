namespace ProductsCRUD.DTOs.ProductCategory
{
    public class ProductCategoryDto
    {
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
    }
}