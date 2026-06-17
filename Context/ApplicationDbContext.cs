using BarberAppointmentServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BarberAppointmentServer.Context;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets (Tablolarımız)
    public DbSet<BarberShop> BarberShops { get; set; }
    public DbSet<ShopMedia> ShopMedias { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<BarberService> BarberServices { get; set; }
    public DbSet<ShopSchedule> ShopSchedules { get; set; }
    public DbSet<BarberSchedule> BarberSchedules { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Identity tablolarının varsayılan ayarları için base metodu mutlaka çağrılmalıdır
        base.OnModelCreating(builder);

        // 1. BarberShop Konfigürasyonları
        builder.Entity<BarberShop>(entity =>
        {
            // Slug alanı URL'de kullanılacağı için benzersiz olmalıdır
            entity.HasIndex(x => x.Slug).IsUnique();

            // Dükkanın bir sahibi (Owner) vardır. 
            // Sahip silinirse dükkanın silinmesini kısıtlıyoruz (veri kaybını önlemek için)
            entity.HasOne(x => x.Owner)
                  .WithMany()
                  .HasForeignKey(x => x.OwnerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // 2. AppUser (Çalışan/Berber) ve BarberShop İlişkisi
        builder.Entity<AppUser>(entity =>
        {
            entity.HasOne(x => x.BarberShop)
                  .WithMany(x => x.Barbers)
                  .HasForeignKey(x => x.BarberShopId)
                  .OnDelete(DeleteBehavior.SetNull); // Dükkan silinirse çalışanların dükkan ID'si null olsun
        });

        // 3. BarberService (Çoktan Çoğa İlişki - Composite Key)
        builder.Entity<BarberService>(entity =>
        {
            // Bir berber aynı hizmeti iki kere ekleyemez
            entity.HasKey(x => new { x.BarberId, x.ServiceId });

            entity.HasOne(x => x.Barber)
                  .WithMany(x => x.BarberServices)
                  .HasForeignKey(x => x.BarberId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Service)
                  .WithMany(x => x.BarberServices)
                  .HasForeignKey(x => x.ServiceId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // 4. Appointment (Randevu) İlişkileri
        builder.Entity<Appointment>(entity =>
        {
            // Müşteri -> Randevu ilişkisi
            entity.HasOne(x => x.Customer)
                  .WithMany(x => x.CustomerAppointments)
                  .HasForeignKey(x => x.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Berber -> Randevu ilişkisi
            entity.HasOne(x => x.Barber)
                  .WithMany(x => x.BarberAppointments)
                  .HasForeignKey(x => x.BarberId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Dükkan -> Randevu ilişkisi
            entity.HasOne(x => x.BarberShop)
                  .WithMany(x => x.Appointments)
                  .HasForeignKey(x => x.BarberShopId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // 5. Payment (Ödeme) Konfigürasyonları
        builder.Entity<Payment>(entity =>
        {
            // Ödeme entegrasyonu sipariş numarası benzersiz olmalıdır
            entity.HasIndex(x => x.MerchantOid).IsUnique();

            // Birebir (One-to-One) İlişki
            entity.HasOne(x => x.Appointment)
                  .WithOne(x => x.Payment)
                  .HasForeignKey<Payment>(x => x.AppointmentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // 6. Global Query Filter (Soft Delete)
        // Veritabanından fiziksel silme yapmayıp IsDeleted=true yapacağımız için,
        // Entity Framework'ün veri çekerken otomatik olarak silinmemiş olanları getirmesini sağlıyoruz.
        builder.Entity<BarberShop>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ServiceCategory>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Service>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Appointment>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ShopMedia>().HasQueryFilter(x => !x.IsDeleted);
    }

    // CreatedAt ve UpdatedAt alanlarının otomatik olarak doldurulması
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                entry.Entity.IsDeleted = false;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}