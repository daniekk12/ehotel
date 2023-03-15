using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;

namespace CodeCool.EhotelBuffet.Simulator.Service;

public class BreakfastSimulator : IDiningSimulator
{
    private static readonly Random Random = new();

    private readonly IBuffetService _buffetService;
    private readonly IReservationManager _reservationManager;
    private readonly IGuestGroupProvider _guestGroupProvider;
    private readonly ITimeService _timeService;

    private readonly List<Guest> _happyGuests = new();
    private readonly List<Guest> _unhappyGuests = new();

    private int _foodWasteCost;
    private int _foodWasteCount;

    public BreakfastSimulator(
        IBuffetService buffetService,
        IReservationManager reservationManager,
        IGuestGroupProvider guestGroupProvider,
        ITimeService timeService)
    {
        _buffetService = buffetService;
        _reservationManager = reservationManager;
        _guestGroupProvider = guestGroupProvider;
        _timeService = timeService;
    }

    public DiningSimulationResults Run(DiningSimulatorConfig config)
    {
        ResetState();
        DateTime currentTime = _timeService.SetCurrentTime(config.Start);
        var guests = _reservationManager.GetGuestsForDate(currentTime);
        int guestsNumber = guests.Count();
        int maximumGuestsPerGroup = 10;
        Console.WriteLine(maximumGuestsPerGroup);
        var guestGroups = _guestGroupProvider.SplitGuestsIntoGroups(guests, config.MinimumGroupCount, maximumGuestsPerGroup);
        _buffetService.Reset();
        
        foreach (var guestGroup in guestGroups)
        {
            _buffetService.Refill(new BasicRefillStrategy());
            foreach (var guest in guestGroup.Guests)
            {
                foreach (var meal in guest.MealPreferences)
                {
                    if (_buffetService.Consume(meal))
                    {
                        _happyGuests.Add(guest);
                    }
                    else
                    {
                        _unhappyGuests.Add(guest);
                    }   
                }
                
            }
            
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Short, currentTime);
            _timeService.IncreaseCurrentTime(config.CycleLengthInMinutes);
        }
        


        // TODO Calculate maximum number of guests per group, using the MinimumGroupCount property of the configuration object.
        
        // TODO Define refill strategy


        return new DiningSimulationResults(currentTime, guests.Count(), _foodWasteCost, _happyGuests, _unhappyGuests);
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }
}
