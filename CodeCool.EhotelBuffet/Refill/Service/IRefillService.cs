using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Refill.Service;

public interface IRefillService
{
    IEnumerable<Portion> AskForRefill(Dictionary<MenuItem, int> menuItemToPortions);
}
