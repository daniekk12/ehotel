using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;

namespace CodeCool.EhotelBuffetTest;

public class Tests
{
    [Test]
    public void GuestProviderTest()
    {
        IGuestProvider guestProvider = new RandomGuestGenerator();
        var guests = guestProvider.Provide(20);
        int expected = 20;
        Assert.That(guests.Count().Equals(expected));
    }
    [Test]
    public void GuestGroupProviderGuestsPerGroupTest()
    {
        IGuestProvider guestProvider = new RandomGuestGenerator();
        IEnumerable<Guest> guests = guestProvider.Provide(20);
        IGuestGroupProvider guestGroupProvider = new GuestGroupProvider();
        int maxLength = 0; 
        IEnumerable<GuestGroup> guestGroups = guestGroupProvider.SplitGuestsIntoGroups(guests, 8, 8);
        foreach (var guestGroup in guestGroups)
        {
            if (guestGroup.Guests.Count()>maxLength)
            {
                maxLength = guestGroup.Guests.Count();
            }
        }
        Assert.That(maxLength < 8 );
    }

    [Test]
    public void GuestGroupProviderGroupCountTest()
    {
        IGuestProvider guestProvider = new RandomGuestGenerator();
        var guests = guestProvider.Provide(20);
        IGuestGroupProvider guestGroupProvider = new GuestGroupProvider();
        IEnumerable<GuestGroup> guestGroups = guestGroupProvider.SplitGuestsIntoGroups(guests, 8, 8);
        Assert.That(guestGroups.Count()==8);
    }
    
}