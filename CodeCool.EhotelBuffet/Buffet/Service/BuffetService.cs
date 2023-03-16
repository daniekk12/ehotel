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

    public void Refill(IRefillStrategy refillStrategy, DateTime currentTime)
    {
        List<Portion> portionsToAdd;
        if (_isInitialized)
        {
            portionsToAdd = _refillService.AskForRefill(refillStrategy.GetRefillQuantities(_currentPortions), currentTime).ToList();
            return;
        }
        _isInitialized = true;
        portionsToAdd = _refillService.AskForRefill(refillStrategy.GetInitialQuantities(_menuProvider.MenuItems), currentTime).ToList();   
        
        foreach (var portion in portionsToAdd)
        {
            _currentPortions.Add(portion);    
        }
    }

    public void Reset()
    {
        _currentPortions.Clear();
        _isInitialized = false;
    }

    public bool Consume(MealType mealType)
    {
        var orderedMeals = _currentPortions.OrderBy(portion => portion.TimeStamp).ToList();
        foreach (var portion in orderedMeals)
        {
            if (portion.MenuItem.MealType==mealType)
            {
                _currentPortions.Remove(portion);
                return true;
            }
        }

        return false;
    }


    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        int sum = 0;
        var portionsToRemove = new List<Portion>();
        foreach (var currentPortion in _currentPortions)
        {
            var durabilityTimeSpan = currentPortion.MenuItem.MealDurabilityInMinutes;
            if (currentPortion.MenuItem.MealDurability == mealDurability)
            {
                var expirationTime = currentPortion.TimeStamp.AddMinutes(durabilityTimeSpan);
                if (expirationTime<=currentDate)
                {
                    sum += currentPortion.MenuItem.Cost;
                    portionsToRemove.Add(currentPortion);
                }
            }
        }
        foreach (var portion in portionsToRemove)
        {
            _currentPortions.Remove(portion);
        }
        return sum;
    }
}