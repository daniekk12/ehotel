using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationManager: IReservationManager
{
    private readonly List<Reservation> _reservations = new();
    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }

    public IEnumerable<Guest> GetGuestsForDate(DateTime date)
    {
        return _reservations.Where(reservation => reservation.Start.Date <= date.Date && reservation.End.Date >= date.Date)
            .Select(reservation => reservation.Guest);
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _reservations.AsEnumerable();
    }
}