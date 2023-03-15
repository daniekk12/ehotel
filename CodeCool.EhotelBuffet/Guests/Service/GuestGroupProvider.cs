using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class GuestGroupProvider : IGuestGroupProvider
{
    public IEnumerable<GuestGroup> SplitGuestsIntoGroups(IEnumerable<Guest> guests, int groupCount,
        int maxGuestPerGroup)
    {
        List<GuestGroup> guestsFinalGroups = new List<GuestGroup>();
        int lastGuestAdded = 0;
        Random random = new Random();
        for (int i = 0; i < groupCount; i++)
        {
            int guestsOnThisGroup = random.Next(0, maxGuestPerGroup);
            List<Guest> guestGroup = new List<Guest>();
            while (guestsOnThisGroup > 0)
            {
                if (lastGuestAdded < guests.Count())
                {
                    guestGroup.Add(guests.ElementAt(lastGuestAdded));
                }
                else
                {
                    break;
                }
                lastGuestAdded++;
                guestsOnThisGroup--;
            }
            GuestGroup guestGroup2 = new GuestGroup(i, guestGroup);
            guestsFinalGroups.Add(guestGroup2);
        }

        return guestsFinalGroups;
    }
}
