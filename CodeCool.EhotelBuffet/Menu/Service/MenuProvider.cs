using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Menu.Service;

public class MenuProvider : IMenuProvider
{
    public IEnumerable<MenuItem> MenuItems { get; } = GetMenuItems();

    private static IEnumerable<MenuItem> GetMenuItems()
    {
        return new[]
        {
            new MenuItem(MealType.ScrambledEggs, 70, MealDurability.Short),
            new MenuItem(MealType.SunnySideUp, 70, MealDurability.Short),
            new MenuItem(MealType.FriedSausage, 100, MealDurability.Short),
            new MenuItem(MealType.FriedBacon, 70, MealDurability.Short),
            new MenuItem(MealType.Pancake, 40, MealDurability.Short),
            new MenuItem(MealType.Croissant, 40, MealDurability.Short),
            new MenuItem(MealType.MashedPotato, 20, MealDurability.Medium),
            new MenuItem(MealType.Muffin, 20, MealDurability.Medium),
            new MenuItem(MealType.Bun, 10, MealDurability.Medium),
            new MenuItem(MealType.Cereal, 30, MealDurability.Long),
            new MenuItem(MealType.Milk, 10, MealDurability.Long),
        };
    }
}
