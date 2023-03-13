using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Refill.Service
{
    public class RefillService : IRefillService
    {
        private readonly IRefillStrategy _refillStrategy;

        public RefillService(IRefillStrategy refillStrategy)
        {
            _refillStrategy = refillStrategy;
        }

        public IEnumerable<Portion> AskForRefill(Dictionary<MenuItem, int> menuItemToPortions)
        {
            var portions = new List<Portion>();

            foreach (var refill in menuItemToPortions)
            {
                var menuItem = refill.Key;
                var portionCount = refill.Value;

                for (var i = 0; i < portionCount; i++)
                {
                    portions.Add(new Portion(menuItem, DateTime.Now));
                }
            }

            return portions;
        }
    }
}