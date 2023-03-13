using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Model;

public record Guest(string Name, GuestType GuestType)
{
    public MealType[] MealPreferences { get; } = GetMealPreferences(GuestType);

    public static MealType[] GetMealPreferences(GuestType guestType)
    {
        MealType[] mealPreferences = new MealType[5];
        if (guestType == GuestType.Business)
        {
            mealPreferences = new MealType[] { MealType.ScrambledEggs, MealType.FriedBacon, MealType.Croissant};
        }

        if (guestType == GuestType.Kid)
        {
            mealPreferences = new MealType[] { MealType.Pancake ,MealType.Muffin,MealType.Cereal,MealType.Milk};
        }

        if (guestType == GuestType.Tourist)
        {
            mealPreferences =
                new MealType[] { MealType.SunnySideUp , MealType.FriedSausage,MealType.MashedPotato,
                    MealType.Bun, MealType.Muffin};
        }

        return mealPreferences;
    }
}
