using Microsoft.EntityFrameworkCore;

namespace projectbridgemvc.Models
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions<MvcDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .Property(x => x.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    AppUserId = 1,
                    FullName = "Admin Kullanıcı",
                    UserName = "admin",
                    Password = "1234",
                    Role = "Admin"
                },
                new AppUser
                {
                    AppUserId = 2,
                    FullName = "Normal Kullanıcı",
                    UserName = "user",
                    Password = "1234",
                    Role = "User"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Gold Taşlı Kolye",
                    Material = "Çelik",
                    Color = "Gold",
                    Price = 350,
                    Stock = 25
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Silver Minimal Yüzük",
                    Material = "Gümüş",
                    Color = "Silver",
                    Price = 220,
                    Stock = 18
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Rose Bileklik",
                    Material = "Çelik",
                    Color = "Rose",
                    Price = 280,
                    Stock = 4
                },
                new Product
                {
                    ProductId = 4,
                    ProductName = "İnci Detaylı Küpe",
                    Material = "Bijuteri",
                    Color = "Beyaz",
                    Price = 180,
                    Stock = 6
                },
                new Product
                {
                    ProductId = 5,
                    ProductName = "Zirkon Taşlı Set",
                    Material = "Gümüş",
                    Color = "Silver",
                    Price = 750,
                    Stock = 3
                }
            );

            modelBuilder.Entity<Sale>().HasData(
                new Sale
                {
                    SaleId = 1,
                    ProductId = 1,
                    Quantity = 2,
                    TotalPrice = 700,
                    SaleDate = new DateTime(2026, 6, 20, 10, 30, 0)
                },
                new Sale
                {
                    SaleId = 2,
                    ProductId = 2,
                    Quantity = 3,
                    TotalPrice = 660,
                    SaleDate = new DateTime(2026, 6, 21, 14, 15, 0)
                },
                new Sale
                {
                    SaleId = 3,
                    ProductId = 3,
                    Quantity = 1,
                    TotalPrice = 280,
                    SaleDate = new DateTime(2026, 6, 22, 16, 45, 0)
                }
            );
        }
    }
}