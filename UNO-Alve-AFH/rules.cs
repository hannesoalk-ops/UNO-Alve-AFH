using System;

namespace UNO_Alve_AFH
{
    internal class Rules
    {
        public bool CanPlay(Card card, Card topCard, string currentColor)
        {
            if (card.type == "Wild" || card.type == "Wild Draw Four")
            {
                return true;
            }

            if (card.color == currentColor)
            {
                return true;
            }

            if (card.type == topCard.type)
            {
                return true;
            }

            return false;
        }

        public string ChooseColor()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose a color:");
                Console.WriteLine("1. Red");
                Console.WriteLine("2. Yellow");
                Console.WriteLine("3. Green");
                Console.WriteLine("4. Blue");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    return "Red";
                }

                if (choice == "2")
                {
                    return "Yellow";
                }

                if (choice == "3")
                {
                    return "Green";
                }

                if (choice == "4")
                {
                    return "Blue";
                }

                Console.WriteLine("Wrong choice.");
            }
        }

        public int GetNextPlayer(int currentPlayer, int direction, int numberOfPlayers)
        {
            currentPlayer = currentPlayer + direction;

            if (currentPlayer >= numberOfPlayers)
            {
                currentPlayer = 0;
            }

            if (currentPlayer < 0)
            {
                currentPlayer = numberOfPlayers - 1;
            }

            return currentPlayer;
        }
    }
}
