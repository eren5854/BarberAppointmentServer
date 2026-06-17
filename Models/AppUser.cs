using BarberAppointmentServer.Enums;
using Microsoft.AspNetCore.Identity;

namespace BarberAppointmentServer.Models;

public class AppUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public UserRoleEnum Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Eğer bir dükkana ait çalışan veya dükkan sahibiyse
    public Guid? BarberShopId { get; set; }
    public virtual BarberShop? BarberShop { get; set; }

    // Navigation Properties
    public virtual ICollection<Appointment> CustomerAppointments { get; set; } = new List<Appointment>();
    public virtual ICollection<Appointment> BarberAppointments { get; set; } = new List<Appointment>();
    public virtual ICollection<BarberSchedule> BarberSchedules { get; set; } = new List<BarberSchedule>();
    public virtual ICollection<BarberService> BarberServices { get; set; } = new List<BarberService>();
}
