using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Reservations.Model;

public record Reservation(DateTime Start, DateTime End, Guest Guest);
