using Microsoft.EntityFrameworkCore;
using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Data;

/// <summary>
/// Contexto principal da aplicação para Entity Framework Core
/// Gerencia todas as entidades do domínio e suas configurações
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets para as entidades principais
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações das entidades
        ConfigureUserEntity(modelBuilder);
        ConfigureCustomerEntity(modelBuilder);
        ConfigureSubscriptionEntity(modelBuilder);
        ConfigurePaymentEntity(modelBuilder);

        // Seed data inicial
        SeedInitialData(modelBuilder);
    }

    /// <summary>
    /// Configura a entidade User com suas propriedades e relacionamentos
    /// </summary>
    private static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();

            // Índices para performance
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.CreatedAt);
        });
    }

    /// <summary>
    /// Configura a entidade Customer com suas propriedades e relacionamentos
    /// </summary>
    private static void ConfigureCustomerEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Document).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();

            // Relacionamento com User (Business Owner)
            entity.HasOne(e => e.BusinessOwner)
                  .WithMany()
                  .HasForeignKey(e => e.BusinessOwnerId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Índices para performance
            entity.HasIndex(e => e.Email);
            entity.HasIndex(e => e.Document);
            entity.HasIndex(e => e.BusinessOwnerId);
            entity.HasIndex(e => e.CreatedAt);
        });
    }

    /// <summary>
    /// Configura a entidade Subscription com suas propriedades e relacionamentos
    /// </summary>
    private static void ConfigureSubscriptionEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(e => e.BillingCycle).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();

            // Relacionamento com Customer
            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Subscriptions)
                  .HasForeignKey(e => e.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Índices para performance
            entity.HasIndex(e => e.CustomerId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.CreatedAt);
        });
    }

    /// <summary>
    /// Configura a entidade Payment com suas propriedades e relacionamentos
    /// </summary>
    private static void ConfigurePaymentEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.TransactionId).HasMaxLength(255);
            entity.Property(e => e.PaymentDate).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();

            // Relacionamento com Subscription
            entity.HasOne(e => e.Subscription)
                  .WithMany(s => s.Payments)
                  .HasForeignKey(e => e.SubscriptionId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Índices para performance
            entity.HasIndex(e => e.SubscriptionId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.PaymentDate);
            entity.HasIndex(e => e.TransactionId);
        });
    }

    /// <summary>
    /// Adiciona dados iniciais para desenvolvimento e testes
    /// </summary>
    private static void SeedInitialData(ModelBuilder modelBuilder)
    {
        // Usuário administrador padrão
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Administrador",
                Email = "admin@gestaomensalidades.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
