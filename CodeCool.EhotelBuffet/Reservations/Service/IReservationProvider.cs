using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public interface IReservationProvider
{
    Reservation Provide(Guest guest, DateTime seasonStart, DateTime seasonEnd);
}
