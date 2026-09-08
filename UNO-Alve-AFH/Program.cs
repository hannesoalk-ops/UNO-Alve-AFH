using System;

public enum CardColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Black
}

// Basklass för alla kort
public abstract class UnoCard
{
    public CardColor Color { get; set; }
}

// 1. Sifferkort (0–9)
public class NumberCard : UnoCard
{
    public int Number { get; set; }

    public NumberCard(CardColor color, int number)
    {
        Color = color;
        Number = number;
    }
}

// 2. Färgade aktionskort (Skip, Reverse, Draw Two)
public class ActionCard : UnoCard
{
    public string Action { get; set; } // "Skip", "Reverse" eller "DrawTwo"

    public ActionCard(CardColor color, string action)
    {
        Color = color;
        Action = action;
    }
}

// 3. Vildkort (Wild, Wild Draw Four)
public class WildCard : UnoCard
{
    public string Effect { get; set; } // "Wild" eller "WildDrawFour"

    public WildCard(string effect)
    {
        Color = CardColor.Black;
        Effect = effect;
    }
}

public class Program
{
    public static void Main()
    {

    }
}
