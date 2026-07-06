namespace projectbridgemvc.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Material { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}