using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationManager: IReservationManager
{
    public List<Reservation> _Reservations = new List<Reservation>();
    public void AddReservation(Reservation reservation)
    {
        _Reservations.Add(reservation);
    }

    public IEnumerable<Guest> GetGuestsForDate(DateTime date)
    {
        return _Reservations.Where(reservation => reservation.Start.Date <= date.Date && reservation.End.Date >= date.Date)
            .Select(reservation => reservation.Guest);
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _Reservations.AsEnumerable();
    }
}