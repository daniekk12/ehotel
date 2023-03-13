using System.Collections;

namespace CodeCool.EhotelBuffet.Guests.Model;

public record GuestGroup(int Id, IEnumerable<Guest> Guests) : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
