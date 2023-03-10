using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Simulator.Model;

public record DiningSimulationResults(
    DateTime Date,
    int TotalGuests,
    int FoodWasteCost,
    IEnumerable<Guest> HappyGuests,
    IEnumerable<Guest> UnhappyGuests);
