using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Reservations.Model;
using CodeCool.EhotelBuffet.Reservations.Service;

namespace CodeCool.EhotelBuffetTest;

public class ReservationsTest
{
    [Test]
    public void ReservationAddTest()
    {
        IReservationProvider reservationProvider = new ReservationProvider(); 
        IReservationManager reservationManager = new ReservationManager();
        IGuestProvider guestProvider = new RandomGuestGenerator();
        var guests = guestProvider.Provide(20);
        DateTime dateTime = new DateTime();
        reservationManager.AddReservation(reservationProvider.Provide(guests.ElementAt(0),
            new DateTime(2023, 03, 10), new DateTime(2023, 03, 13)));
        reservationManager.AddReservation(reservationProvider.Provide(guests.ElementAt(1),
            new DateTime(2023, 03, 12), new DateTime(2023, 03, 15)));
        reservationManager.AddReservation(reservationProvider.Provide(guests.ElementAt(2),
            new DateTime(2023, 03, 09), new DateTime(2023, 03, 13)));
        reservationManager.AddReservation(reservationProvider.Provide(guests.ElementAt(3),
            new DateTime(2023, 03, 09), new DateTime(2023, 03, 15)));
        IEnumerable<Reservation> allReservations = reservationManager.GetAll();
        Assert.That(allReservations.Count()==4);
    }

    [Test]
    public void ReservationGetGuestForDateTest()
    {
        IReservationManager reservationManager = new ReservationManager();
        IGuestProvider guestProvider = new RandomGuestGenerator();
        var guests = guestProvider.Provide(20).ToList();
        reservationManager.AddReservation(new Reservation(new DateTime(2022,12,31),new DateTime(2023,12,31), guests[0]));
        var guestsForDate = reservationManager.GetGuestsForDate(new DateTime(2023,8,30)).ToList();
        Assert.That(guests.Contains(guestsForDate[0]));
    }

    [Test]
    public void ReservationProviderProvidesReservationTest()
    {
        Guest guest = new Guest("Coco", GuestType.Business);
        IReservationManager reservationManager = new ReservationManager();
        reservationManager.AddReservation(new Reservation(new DateTime(2022,12,31),new DateTime(2023,12,31),guest));
        IEnumerable<Reservation> allReservations = reservationManager.GetAll();
        Assert.That(allReservations.ElementAt(0).Guest.Name, Is.EqualTo("Coco"));
    }
}