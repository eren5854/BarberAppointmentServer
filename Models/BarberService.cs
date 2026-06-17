namespace BarberAppointmentServer.Models;

public class BarberService
{
    public Guid BarberId { get; set; }
    public virtual AppUser Barber { get; set; } = null!;

    public Guid ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;
}
