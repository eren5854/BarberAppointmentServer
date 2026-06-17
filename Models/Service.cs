namespace BarberAppointmentServer.Models;

public class Service : BaseEntity
{
    public Guid ServiceCategoryId { get; set; }
    public virtual ServiceCategory ServiceCategory { get; set; } = null!;

    public required string Name { get; set; }
    public int DurationInMinutes { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual ICollection<BarberService> BarberServices { get; set; } = new List<BarberService>();
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
