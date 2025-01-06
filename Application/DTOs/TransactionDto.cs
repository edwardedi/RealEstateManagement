namespace Application.DTOs
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public double SalePrice { get; set; }
    }
}
