using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public interface IReservationManager
{
    void AddReservation(Reservation reservation);
    IEnumerable<Guest> GetGuestsForDate(DateTime date);
    IEnumerable<Reservation> GetAll();
}
