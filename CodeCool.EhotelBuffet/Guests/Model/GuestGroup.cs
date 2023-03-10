namespace CodeCool.EhotelBuffet.Guests.Model;

public record GuestGroup(int Id, IEnumerable<Guest> Guests);
