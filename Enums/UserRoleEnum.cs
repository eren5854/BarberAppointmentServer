namespace BarberAppointmentServer.Enums;

public enum UserRoleEnum
{
    SuperAdmin,   // Sen (Sistem Genel Yöneticisi)
    ShopOwner,    // Berber Dükkanı Sahibi (Tenant Admin)
    Barber,       // Dükkandaki Çalışan Berber
    Customer      // Randevu Alan Müşteri
}
