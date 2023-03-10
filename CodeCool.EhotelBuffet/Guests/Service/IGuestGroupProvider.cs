using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public interface IGuestGroupProvider
{
    IEnumerable<GuestGroup> SplitGuestsIntoGroups(IEnumerable<Guest> guests, int groupCount, int maxGuestPerGroup);
}
