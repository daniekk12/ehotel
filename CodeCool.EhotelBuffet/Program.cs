using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Service;
using CodeCool.EhotelBuffet.Ui;

ITimeService timeService = new TimeService();
IMenuProvider menuProvider = new MenuProvider();
IRefillService refillService = null;
IGuestGroupProvider guestGroupProvider = null;
IReservationProvider reservationProvider = null;
IReservationManager reservationManager = null;

IBuffetService buffetService = new BuffetService(menuProvider, refillService);
IDiningSimulator diningSimulator =
    new BreakfastSimulator(buffetService, reservationManager, guestGroupProvider, timeService);

EhoteBuffetUi ui = new EhoteBuffetUi(reservationProvider, reservationManager, diningSimulator);
IGuestProvider guestProvider = new RandomGuestGenerator();
ui.Run();
var guests = guestProvider.Provide(20);


var guestsgroups = new GuestGroupProvider().SplitGuestsIntoGroups(guests, 7, 10);
foreach (var guestGroup in guestsgroups)
{
    Console.WriteLine(guestGroup.Id);
    foreach (var guestGroupGuest in guestGroup.Guests)
    {
        Console.WriteLine(guestGroupGuest.Name);
    }
}
