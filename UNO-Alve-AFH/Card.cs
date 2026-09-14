using System;

namespace UNO_Alve_AFH
{
    internal class Card
    {
        public string color;
        public string type;

        public Card(string color, string type)
        {
            this.color = color;
            this.type = type;
        }

        public string GetName()
        {
            if (color == "")
            {
                return type;
            }

            return color + " " + type;
        }
    }
}