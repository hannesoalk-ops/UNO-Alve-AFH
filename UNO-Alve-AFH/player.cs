using System;
using System.Collections.Generic;

namespace UNO_Alve_AFH
{
    internal class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();

        public Player(string name)
        {
            this.name = name;
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
            for (int i = 0; i < hand.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + hand[i].GetName());
            }
        }
    }
}