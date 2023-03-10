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
        return null;
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }
}
