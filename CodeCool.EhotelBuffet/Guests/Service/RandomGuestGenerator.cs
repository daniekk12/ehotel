﻿using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class RandomGuestGenerator : IGuestProvider
{
    private static readonly Random Random = new();

    private static readonly string[] Names =
    {
        "Georgia", "Savannah", "Phoenix", "Winona", "Carol", "Brooklyn", "Talullah", "Scarlett", "Ruby", "Lola",
        "Cleo", "Beatrix", "Mika", "Everly", "Kiki", "Rae", "Arya", "Elsa", "Lulu", "Zelda",
        "Felix", "Finn", "Theo", "Hugo", "Archie", "Magnus", "Lucian", "Enzo", "Otto", "Nico", "Rhys",
        "Rupert", "Hugh", "Finley", "Ralph", "Lewis", "Wilbur", "Alfie", "Ernest", "Chester", "Ziggy"
    };

    public IEnumerable<Guest> Provide(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            yield return GenerateRandomGuest();
        }
    }

    private static Guest GenerateRandomGuest()
    {
        return new Guest(GetRandomName(), GetRandomType());
    }

    private static string GetRandomName()
    {
        
        return Names[Random.Next(0,Names.Length-1)];
    }

    private static GuestType GetRandomType()
    {
        Array guestType = Enum.GetValues(typeof(GuestType));
        var i = Random.Next(guestType.Length);
        return (GuestType)guestType.GetValue(i);
    }
}
