using System;
using System.Collections.Generic;

namespace UNO_Alve_AFH
{
    internal class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();

        // Spelarens emoji
        public string emoji;

        // Spelarens färg
        public ConsoleColor playerColor;

        public Player(string name, int playerNumber)
        {
            this.name = name;

            // Olika emoji beroende på spelare
            if (playerNumber == 1)
            {
                emoji = "😎";
                playerColor = ConsoleColor.Cyan;
            }
            else if (playerNumber == 2)
            {
                emoji = "😈";
                playerColor = ConsoleColor.Red;
            }
            else if (playerNumber == 3)
            {
                emoji = "🤖";
                playerColor = ConsoleColor.Green;
            }
            else
            {
                emoji = "👽";
                playerColor = ConsoleColor.Yellow;
            }
        }

        public void DrawCard(Deck deck)
        {
            Card card = deck.DrawCard();
            hand.Add(card);
        }

        public void DrawCards(Deck deck, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                DrawCard(deck);
            }
        }

        public Card PlayCard(int number)
        {
            Card card = hand[number];
            hand.RemoveAt(number);

            return card;
        }

        public void ShowHand()
        {
            Console.ForegroundColor = playerColor;

            Console.WriteLine();
            Console.WriteLine(emoji + " " + name + "'s hand:");

            Console.ResetColor();

            for (int i = 0; i < hand.Count; i++)
            {
                Console.Write((i + 1) + ". ");

                // Visa kortet i kortets färg
                if (hand[i].color == "Red")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (hand[i].color == "Blue")
                    Console.ForegroundColor = ConsoleColor.Blue;
                else if (hand[i].color == "Green")
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (hand[i].color == "Yellow")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(hand[i].GetName());

                Console.ResetColor();
            }
        }

        public void ShowPlayer()
        {
            Console.ForegroundColor = playerColor;

            Console.WriteLine(emoji + " " + name);

            Console.ResetColor();
        }
    }
}