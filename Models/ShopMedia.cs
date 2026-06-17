using Microsoft.AspNetCore.Mvc.Formatters;

namespace BarberAppointmentServer.Models;

public class ShopMedia : BaseEntity
{
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = null!;

    public required string MediaUrl { get; set; }
    public MediaType Type { get; set; }
    public int DisplayOrder { get; set; } // Vitrindeki sıralama için
}