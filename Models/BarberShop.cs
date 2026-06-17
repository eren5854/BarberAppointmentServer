namespace BarberAppointmentServer.Models;

public class BarberShop : BaseEntity
{
    public required string Name { get; set; }

    // URL uzantısı için (Örn: "janti-erkek-kuaforu")
    public required string Slug { get; set; }

    public string? Description { get; set; }
    public string? Address { get; set; }

    // Harita ve konum bazlı aramalar için koordinatlar
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; } = true;

    // Dükkanın sahibi (ShopOwner Rolündeki Kullanıcı)
    public Guid OwnerId { get; set; }
    public virtual AppUser Owner { get; set; } = null!;

    // Navigation Properties
    public virtual ICollection<AppUser> Barbers { get; set; } = new List<AppUser>();
    public virtual ICollection<ServiceCategory> Categories { get; set; } = new List<ServiceCategory>();
    public virtual ICollection<ShopMedia> Medias { get; set; } = new List<ShopMedia>();
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<ShopSchedule> ShopSchedules { get; set; } = new List<ShopSchedule>();
}
