using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace RealEstateManagement.IntegrationTests
{
    public class TestDbContextFactory
    {
        public static ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting") 
            .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            SeedTestData(context);

            return context;
        }
        private static void SeedTestData(ApplicationDbContext context)
        {
            if (!context.PropertyListings.Any()) // Avoid duplicate seeds
            {
                context.Users.Add(new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "John Doe",
                    Email = "John@gmail.com",
                    PhoneNumber = "0721345678",
                    PasswordHash = "hashed_password"
                });
                context.Users.Add(new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Jane Doe",
                    Email = "jane@gmail.com",
                    PhoneNumber = "0721345677",
                    PasswordHash = "hashed_password2"
                });

                context.ClientInquiries.Add(new ClientInquiry
                {
                    InquiryId = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    MinPrice = 100000,
                    MaxPrice = 200000,
                    MinSquareFootage = 1000,
                    MaxSquareFootage = 2000,
                    NumberOfBedrooms = 3,
                    NumberOfBathrooms = 2
                });

                context.Transactions.Add(new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    PropertyId = Guid.NewGuid(),
                    BuyerId = Guid.NewGuid(),
                    SellerId = Guid.NewGuid(),
                    SalePrice = 200000
                });

                context.PropertyListings.Add(new PropertyListing
                {
                    PropertyId = Guid.NewGuid(),
                    Title = "Property 1",
                    Address = "123 Main Street",
                    Type = "House",
                    Price = 100000,
                    SquareFootage = 2000,
                    NumberOfBedrooms = 3,
                    NumberOfBathrooms = 2,
                    Description = "A beautiful house",
                    Status = "Available",
                    ListingDate = DateTime.Now,
                    ImageURLs = "https://www.google.com",
                    UserID = Guid.NewGuid()
                });
            }
            context.SaveChanges();
        }
    }
    
}