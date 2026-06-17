using Microsoft.AspNetCore.Http.HttpResults;

namespace BarberAppointmentServer.Models;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
        IsDeleted = false;
        IsActived = true;
    }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActived { get; set; }
}
