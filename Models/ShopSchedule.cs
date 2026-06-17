namespace BarberAppointmentServer.Models;

public class ShopSchedule : BaseEntity
{
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public bool IsClosed { get; set; } = false; // Dükkan o gün tamamen kapalı mı?
}