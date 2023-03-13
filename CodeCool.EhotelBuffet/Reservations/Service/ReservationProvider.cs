using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationProvider: IReservationProvider
{
    public Reservation Provide(Guest guest, DateTime seasonStart, DateTime seasonEnd)
    {
        DateTime startReservation = GetRandomDateTime(seasonStart, seasonEnd);
        DateTime endReservation = GetRandomDateTime(startReservation, seasonEnd);
        return new Reservation(startReservation, endReservation, guest);
    }

    public DateTime GetRandomDateTime( DateTime seasonStart,DateTime seasonEnd)
    {
        Random random = new Random();
        TimeSpan timeSpan = seasonEnd - seasonStart;
        DateTime randomDate = seasonStart.AddDays(random.Next(timeSpan.Days));
        return randomDate;
    }
}