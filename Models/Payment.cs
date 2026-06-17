using BarberAppointmentServer.Enums;

namespace BarberAppointmentServer.Models;

public class Payment : BaseEntity
{
    public Guid AppointmentId { get; set; }
    public virtual Appointment Appointment { get; set; } = null!;

    public required string MerchantOid { get; set; } // Ödeme ağ geçidi token/sipariş numarası
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "TRY";
    public PaymentStatusEnum Status { get; set; } = PaymentStatusEnum.Pending;
    public DateTimeOffset TransactionDate { get; set; } = DateTimeOffset.UtcNow;
}