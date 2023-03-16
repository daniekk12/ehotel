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
        var guests = _reservationManager.GetGuestsForDate(currentTime).ToList();
        int guestsNumber = guests.Count;
        var refillStrategy = new BasicRefillStrategy();
        if (guestsNumber == 0 || guestsNumber < config.MinimumGroupCount)
        {
            Console.WriteLine("No guests on this day!");
            _buffetService.Refill(refillStrategy, currentTime);
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Short, currentTime);
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Medium, currentTime);
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Long, currentTime);
            return new DiningSimulationResults(currentTime, guestsNumber, _foodWasteCost, _happyGuests, _unhappyGuests);
        }
        // Console.WriteLine($"NumberOfGuests:{guests.Count}");
        // Console.WriteLine(guestDiv);
        int groupCount = new Random().Next(config.MinimumGroupCount, guestsNumber);
        double guestDiv = 10;
        int maximumGuestsPerGroup = (int)Math.Ceiling(guestDiv);
        var guestGroups = _guestGroupProvider.SplitGuestsIntoGroups(guests, groupCount, maximumGuestsPerGroup);
        int breakfastGuests = 0;
        currentTime = _timeService.IncreaseCurrentTime(config.CycleLengthInMinutes);
        foreach (var guestGroup in guestGroups)
        {
            _buffetService.Refill(refillStrategy, currentTime);
            foreach (var guest in guestGroup.Guests)
            {
                Console.WriteLine(guest.Name);
                bool foundMeal = false;
                foreach (var meal in guest.MealPreferences)
                {
                    if (_buffetService.Consume(meal))
                    {
                        _buffetService.Consume(meal);
                        foundMeal = true;
                    }                    
                }

                if (foundMeal)
                {
                    _happyGuests.Add(guest);
                    breakfastGuests++;
                }
                else
                {
                    _unhappyGuests.Add(guest);
                    breakfastGuests++;
                }
            }
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Short, currentTime);
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Medium, currentTime);
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Long, currentTime);
            currentTime = _timeService.IncreaseCurrentTime(config.CycleLengthInMinutes);
        }
        


        // TODO Calculate maximum number of guests per group, using the MinimumGroupCount property of the configuration object.
        
        // TODO Define refill strategy


        return new DiningSimulationResults(currentTime, guestsNumber, _foodWasteCost, _happyGuests, _unhappyGuests);
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }

    // private int CalculateMaximumGuestsPerGroup(int minimumGroupCount, int guestCount)
    // {
    //     
    //     while (guestCount > 0)
    //     {
    //         
    //     }
    // }
}
