namespace BarberAppointmentServer.Models;

public class BarberSchedule : BaseEntity
{
    public Guid BarberId { get; set; }
    public virtual AppUser Barber { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsDayOff { get; set; } = false; // Berber izinli mi?
}