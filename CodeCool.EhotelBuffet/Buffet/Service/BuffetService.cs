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
    }

    public void Reset()
    {
    }

    public bool Consume(MealType mealType)
    {
        return false;
    }


    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        return 0;
    }
}
