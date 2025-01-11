using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string uuid_opt = "uuid_generate_v4()";
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<PropertyListing> PropertyListings { get; set; }
        public DbSet<ClientInquiry> ClientInquiries { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<PropertyListing>(

                    entity =>
                    {
                        entity.ToTable("PropertyListings");
                        entity.HasKey(e => e.PropertyId);
                        entity.Property(e => e.PropertyId).HasColumnType("uuid").HasDefaultValueSql(uuid_opt).ValueGeneratedOnAdd();
                        entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                        entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.Type).IsRequired();
                        entity.Property(e => e.Price).IsRequired();
                        entity.Property(e => e.SquareFootage).IsRequired();
                        entity.Property(e => e.NumberOfBedrooms).IsRequired();
                        entity.Property(e => e.NumberOfBathrooms).IsRequired();
                        entity.Property(e => e.Description).IsRequired().HasMaxLength(400);
                        entity.Property(e => e.Status).IsRequired();
                        entity.Property(e => e.ListingDate).IsRequired();
                        entity.Property(e => e.ImageURLs).IsRequired();
                        entity.Property(e => e.UserID).IsRequired();

                    }
                );
            
            modelBuilder.Entity<ClientInquiry>(
                entity =>
                {
                    entity.ToTable("ClientInquiries");
                    entity.HasKey(e => e.InquiryId);
                    entity.Property(e => e.InquiryId).HasColumnType("uuid").HasDefaultValueSql(uuid_opt).ValueGeneratedOnAdd();
                    entity.Property(e => e.ClientId).IsRequired();

                });
            
            modelBuilder.Entity<Transaction>(
                entity =>
                {
                    entity.ToTable("Transactions");
                    entity.HasKey(e => e.TransactionId);
                    entity.Property(e => e.TransactionId).HasColumnType("uuid").HasDefaultValueSql(uuid_opt).ValueGeneratedOnAdd();
                    entity.Property(e => e.PropertyId).IsRequired();
                    entity.Property(e => e.BuyerId).IsRequired();
                    entity.Property(e => e.SellerId).IsRequired();
                    entity.Property(e => e.SalePrice).IsRequired();

                    entity.HasOne<PropertyListing>()
                        .WithMany()
                        .HasForeignKey(e => e.PropertyId)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne<User>()
                        .WithMany()
                        .HasForeignKey(e => e.BuyerId)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne<User>()
                        .WithMany()
                        .HasForeignKey(e => e.SellerId)
                        .OnDelete(DeleteBehavior.Cascade);

                });

            modelBuilder.Entity<User>(
                entity =>
                {
                    entity.ToTable("Users");
                    entity.HasKey(e => e.UserId);
                    entity.Property(e => e.UserId).HasColumnType("uuid").HasDefaultValueSql(uuid_opt).ValueGeneratedOnAdd();
                    entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                    entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.Property(e => e.PhoneNumber).IsRequired();
                    entity.Property(e => e.PasswordHash).IsRequired();

                });
        }
    }
}
