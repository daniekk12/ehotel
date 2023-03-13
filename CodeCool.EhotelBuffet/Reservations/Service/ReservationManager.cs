using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationManager: IReservationManager
{
    public List<Reservation> _Reservations= new List<Reservation>() ;
    public void AddReservation(Reservation reservation)
    {
        _Reservations.Add(reservation);
    }

    public IEnumerable<Guest> GetGuestsForDate(DateTime date)
    {
        List<Guest> GuestsForDate = new List<Guest>();

        foreach (var reservation in _Reservations)
        {
            if (date.CompareTo(reservation.Start) >= 0 && date.CompareTo(reservation.End) <= 0)
            {
                GuestsForDate.Add(reservation.Guest);
            }
        }

        return GuestsForDate.AsEnumerable();
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _Reservations.AsEnumerable();
    }
}