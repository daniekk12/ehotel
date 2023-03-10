using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Refill.Service;

public interface IRefillStrategy
{
    Dictionary<MenuItem, int> GetInitialQuantities(IEnumerable<MenuItem> menuItems);
    Dictionary<MenuItem, int> GetRefillQuantities(IEnumerable<Portion> currentPortions);
}
