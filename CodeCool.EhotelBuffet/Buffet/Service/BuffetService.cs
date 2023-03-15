using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;

namespace CodeCool.EhotelBuffet.Buffet.Service;

public class BuffetService : IBuffetService
{
    private readonly IMenuProvider _menuProvider;
    private readonly IRefillService _refillService;
    private readonly List<Portion> _currentPortions = new();

    private bool _isInitialized;

    public BuffetService(IMenuProvider menuProvider, IRefillService refillService)
    {
        _menuProvider = menuProvider;
        _refillService = refillService;
    }

    public void Refill(IRefillStrategy refillStrategy)
    {
        if (!_isInitialized)
        {
            var menuItems = _menuProvider.MenuItems;

            var enumerable = menuItems as MenuItem[] ?? menuItems.ToArray();
            var initialQuantities = refillStrategy.GetInitialQuantities(enumerable);
            
            foreach (var menuItem in enumerable)
            {
                foreach (var keyValuePair in initialQuantities)
                {
                    if (keyValuePair.Key.Equals(menuItem))
                    {
                        _currentPortions.Add(new Portion(menuItem, DateTime.Now));
                    }
                }
            }

            _isInitialized = true;
        }
        else
        {
            var menuItems = _menuProvider.MenuItems;
            var refillQuantities = refillStrategy.GetRefillQuantities(_currentPortions);
            foreach (var menuItem in menuItems)
            {
                foreach (var refillQuantity in refillQuantities)
                {
                    if (refillQuantity.Key.Equals(menuItem))
                    {
                        _currentPortions.Add(new Portion(menuItem, DateTime.Now));
                    }
                }

            }
        }
    }

    public void Reset()
    {
        _currentPortions.Clear();
        _isInitialized = false;
    }

    public bool Consume(MealType mealType)
    {
        var portionsToConsume = _currentPortions
            .Where(p => p.MenuItem.MealType == mealType)
            .OrderByDescending(p => p.TimeStamp);

        if (!portionsToConsume.Any())
        {
            return false;
        }

        _currentPortions.Remove(portionsToConsume.First());

        return true;
    }


    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        var portionsToDiscard = _currentPortions
            .Where(p => p.TimeStamp < currentDate && p.MenuItem.MealDurability == mealDurability)
            .ToList();

        foreach (var portion in portionsToDiscard)
        {
            _currentPortions.Remove(portion);
        }

        return portionsToDiscard.Sum(p => p.MenuItem.Cost);
    }
}
