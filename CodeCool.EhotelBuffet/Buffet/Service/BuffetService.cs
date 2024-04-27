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
        if (_isInitialized)
        {
            _refillService.AskForRefill(refillStrategy.GetRefillQuantities(_currentPortions));
            return;
        }

        _refillService.AskForRefill(refillStrategy.GetInitialQuantities(_menuProvider.MenuItems));
    }

    public void Reset()
    {
        _currentPortions.Clear();
        _isInitialized = false;
    }

    public bool Consume(MealType mealType)
    {
        var orderedMeals = _currentPortions.OrderBy(portion => portion.TimeStamp);
        foreach (var portion in orderedMeals)
        {
            if (portion.MenuItem.MealType==mealType)
            {
                return true;
            }
        }

        return false;
    }


    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        int sum = 0;
        foreach (var currentPortion in _currentPortions)
        {
            if (currentPortion.MenuItem.MealDurability == mealDurability)
            {
                if (currentPortion.TimeStamp.AddMinutes(currentPortion.MenuItem.MealDurabilityInMinutes) >= currentDate)
                {
                    sum += currentPortion.MenuItem.Cost;
                    _currentPortions.Remove(currentPortion);
                }
            }
        }

        return sum;
    }
}