namespace ProductsCRUD.DTOs.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }
    }
}