using System.Collections.Generic;
using System.Linq;
using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Refill.Service
{
    public class BasicRefillStrategy : IRefillStrategy
    {
        private int OptimalPortionCount = 20;

        // public BasicRefillStrategy(int expectedGuests)
        // {
        //     OptimalPortionCount = expectedGuests;
        // }

        public Dictionary<MenuItem, int> GetInitialQuantities(IEnumerable<MenuItem> menuItems)
        {
            var ret = new Dictionary<MenuItem, int>();
            foreach (var menuItem in menuItems)
            {
                ret.Add(menuItem, OptimalPortionCount);
            }

            return ret;
        }

        public Dictionary<MenuItem, int> GetRefillQuantities(IEnumerable<Portion> currentPortions)
        {
            var groupedByMenuItem = currentPortions.GroupBy(p => p.MenuItem);
            var refillQuantities = new Dictionary<MenuItem, int>();

            foreach (var grouping in groupedByMenuItem)
            {
                var menuItem = grouping.Key;
                var portionCount = grouping.Count();
                var refillCount = OptimalPortionCount - portionCount;

                if (refillCount > 0)
                {
                    refillQuantities.Add(menuItem, refillCount);
                }
            }

            return refillQuantities;
        }
    }
}