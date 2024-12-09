namespace Domain.Entities
{
    public class PropertyListing
    {
        public Guid PropertyId { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? Type { get; set; }
        public double Price { get; set; }
        public double SquareFootage { get; set; }
        public double NumberOfBedrooms { get; set; }
        public double NumberOfBathrooms { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime ListingDate { get; set; }
        public string? ImageURLs { get; set; }
        public Guid UserID { get; set; }

    }
}
