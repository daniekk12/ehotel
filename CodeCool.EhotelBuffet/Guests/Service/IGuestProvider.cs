using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public interface IGuestProvider
{
    IEnumerable<Guest> Provide(int quantity);
}
