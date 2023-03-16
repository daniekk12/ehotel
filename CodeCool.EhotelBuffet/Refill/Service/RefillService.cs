using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Refill.Service;

public class RefillService : IRefillService
{
    public IEnumerable<Portion> AskForRefill(Dictionary<MenuItem, int> menuItemToPortions, DateTime currentTime)
    {
        List<Portion> portions = new List<Portion>();
        foreach (var itemToPortion in menuItemToPortions)
        {
            for (int i = 0; i < itemToPortion.Value; i++)
            {
                portions.Add(new Portion(itemToPortion.Key, currentTime));
            }
        }

        return portions;
    }
}

