using BarberAppointmentServer.Enums;

namespace BarberAppointmentServer.Models;

public class Appointment : BaseEntity
{
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = null!;

    public Guid CustomerId { get; set; }
    public virtual AppUser Customer { get; set; } = null!;

    public Guid BarberId { get; set; }
    public virtual AppUser Barber { get; set; } = null!;

    public Guid ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;

    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public AppointmentStatusEnum Status { get; set; } = AppointmentStatusEnum.Pending;
    public decimal TotalPrice { get; set; }
    public string? Notes { get; set; }

    public virtual Payment? Payment { get; set; }
}
