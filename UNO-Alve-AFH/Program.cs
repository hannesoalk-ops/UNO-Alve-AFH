using System;

public enum CardColor
{
    Red,
    Green,
    Blue,
    Yellow,
    Black,
}

public enum CardType
{
    None,
    Reverse,
    Skip,
    Draw2,
    ColorChange,
    Draw4,

}

public class card
{
    public CardColor Color { get; set; }
    public CardType Type { get; set; }
    public int? Number { get; set; }

    public card(CardColor color, CardType type, int? number = null)
    {
        Color = color;
        Type = type;
        Number = number;    
    }
}
