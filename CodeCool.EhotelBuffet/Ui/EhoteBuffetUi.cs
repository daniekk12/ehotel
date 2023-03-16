﻿using System.Threading.Channels;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;
using CodeCool.EhotelBuffet.Simulator.Service;

namespace CodeCool.EhotelBuffet.Ui;

public class EhoteBuffetUi
{
    private readonly IReservationManager _reservationManager;
    private readonly IDiningSimulator _diningSimulator;
    private readonly IReservationProvider _reservationProvider;
    private readonly IGuestProvider _guestProvider = new RandomGuestGenerator();

    public EhoteBuffetUi(
        IReservationProvider reservationProvider,
        IReservationManager reservationManager,
        IDiningSimulator diningSimulator)
    {
        _reservationProvider = reservationProvider;
        _reservationManager = reservationManager;
        _diningSimulator = diningSimulator;
    }

    public void Run()
    {
        int guestCount = 20;
        DateTime seasonStart = DateTime.Today;
        DateTime seasonEnd = DateTime.Today.AddDays(3);

        var guests = GetGuests(guestCount).ToList();
        CreateReservations(guests, seasonStart, seasonEnd);

        PrintGuestsWithReservations();

        var currentDate = seasonStart;

        while (currentDate <= seasonEnd)
        {
            var simulatorConfig = new DiningSimulatorConfig(
                currentDate.AddHours(6),
                currentDate.AddHours(10),
                30,
                3);

            var results = _diningSimulator.Run(simulatorConfig);
            PrintSimulationResults(results);
            currentDate = currentDate.AddDays(1);
        }

        Console.ReadLine();
    }

    private IEnumerable<Guest> GetGuests(int quantity)
    {
        var guests = _guestProvider.Provide(quantity);
        return guests;
    }

    private void CreateReservations(IEnumerable<Guest> guests, DateTime seasonStart, DateTime seasonEnd)
    {
        foreach (var guest in guests)
        {
            var reservation = _reservationProvider.Provide(guest, seasonStart, seasonEnd);
            _reservationManager.AddReservation(reservation);
            
        }
    }

    private void PrintGuestsWithReservations()
    {
        var allReservations = _reservationManager.GetAll();
        foreach (var reservation in allReservations)
        {
            Console.WriteLine($"------------\n" +
                              $"Name: {reservation.Guest.Name}\n" +
                              $"Start date: {reservation.Start}\n" +
                              $"End date: {reservation.End}");
        }
    }

    private static void PrintSimulationResults(DiningSimulationResults results)
    {
        Console.WriteLine("-----------------");
        Console.WriteLine("Results");
        Console.WriteLine($"Date: {results.Date}");
        Console.WriteLine($"Total Guests: {results.TotalGuests}");
        Console.WriteLine($"Happy Guests: {results.HappyGuests.Count()}");
        Console.WriteLine($"Unhappy Guests: {results.UnhappyGuests.Count()}");
        Console.WriteLine($"Waste cost: {results.FoodWasteCost}");
        Console.WriteLine("-----------------");
    }
}
