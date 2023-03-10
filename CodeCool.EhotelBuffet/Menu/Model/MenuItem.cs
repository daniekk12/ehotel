namespace CodeCool.EhotelBuffet.Menu.Model;

public record MenuItem(MealType MealType, int Cost, MealDurability MealDurability)
{
    public int MealDurabilityInMinutes { get; } = MealDurability switch
    {
        MealDurability.Short => 30,
        MealDurability.Medium => 120,
        MealDurability.Long => 360,
        _ => 0
    };
}
