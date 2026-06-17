namespace BarberAppointmentServer.Models;

public class ServiceCategory : BaseEntity
{
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = null!;

    public required string Name { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}