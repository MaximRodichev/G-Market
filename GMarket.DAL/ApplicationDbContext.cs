using System.Text.Json;
using GMarket.Domain.Entities.Forum;
using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Entities.Market;
using GMarket.Domain.Types;
using GMarket.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GMarket.DAL;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<UserSecurity> UserSecurities { get; set; }
    public DbSet<CustomerContext> CustomerContexts { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleCommentary> ArticleCommentaries { get; set; }
    public DbSet<FavoriteCategory> FavoriteCategories { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=GMarket;Username=postgres;Password=12345",
                options => options.MigrationsAssembly("GMarket.DAL"));
        }
    }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductItem) // Связь "один-к-одному"
            .WithOne(pi => pi.Product) // Обратная связь
            .HasForeignKey<ProductItem>(pi => pi.ProductId); // Укажите внешний ключ

        modelBuilder.Entity<UserSecurity>(entity =>
        {
            entity.HasOne(us => us.Context)
                .WithOne(c => c.UserSecurity)
                .HasForeignKey<CustomerContext>(c => c.UserSecurityId);
        });

        modelBuilder.Entity<CustomerContext>(entity =>
        {
            entity.HasIndex(c => c.UserSecurityId).IsUnique();
        });
        
        modelBuilder.Entity<FavoriteCategory>()
            .Property(fc => fc.ProductIds)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), // Сериализация
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions)null)) // Десериализация
            .HasColumnType("jsonb"); // Укажите тип JSONB (для PostgreSQL)
        
        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Address)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Address>(v, (JsonSerializerOptions)null)
            )
            .HasColumnType("jsonb"); // Specify JSONB type for PostgreSQL
    }
}