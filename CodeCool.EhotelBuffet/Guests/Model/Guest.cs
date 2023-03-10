using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Model;

public record Guest(string Name, GuestType GuestType)
{
    public MealType[] MealPreferences { get; } = Array.Empty<MealType>();
}
