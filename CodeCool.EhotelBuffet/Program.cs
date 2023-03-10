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

ui.Run();
