namespace projectbridgemvc.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime SaleDate { get; set; }
    }
}