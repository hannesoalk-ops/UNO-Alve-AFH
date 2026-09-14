using System;
using System.Collections.Generic;

namespace UNO_Alve_AFH
{
    internal class Deck
    {
        public List<Card> cards = new List<Card>();
        public List<Card> discardPile = new List<Card>();

        Random random = new Random();

        public Deck()
        {
            CreateDeck();
            Shuffle();
        }

        public void CreateDeck()
        {
            string[] colors = { "Red", "Yellow", "Green", "Blue" };

            for (int i = 0; i < colors.Length; i++)
            {
                cards.Add(new Card(colors[i], "0"));

                for (int number = 1; number <= 9; number++)
                {
                    cards.Add(new Card(colors[i], number.ToString()));
                    cards.Add(new Card(colors[i], number.ToString()));
                }

                cards.Add(new Card(colors[i], "Skip"));
                cards.Add(new Card(colors[i], "Skip"));

                cards.Add(new Card(colors[i], "Reverse"));
                cards.Add(new Card(colors[i], "Reverse"));

                cards.Add(new Card(colors[i], "Draw Two"));
                cards.Add(new Card(colors[i], "Draw Two"));
            }

            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card("", "Wild"));
                cards.Add(new Card("", "Wild Draw Four"));
            }
        }

        public void Shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int randomNumber = random.Next(i + 1);

                Card temp = cards[i];
                cards[i] = cards[randomNumber];
                cards[randomNumber] = temp;
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0)
            {
                RefillDeck();
            }

            Card card = cards[0];
            cards.RemoveAt(0);

            return card;
        }

        public void AddToDiscard(Card card)
        {
            discardPile.Add(card);
        }

        public Card GetTopCard()
        {
            return discardPile[discardPile.Count - 1];
        }

        public void RefillDeck()
        {
            if (discardPile.Count <= 1)
            {
                return;
            }

            Card topCard = discardPile[discardPile.Count - 1];

            for (int i = 0; i < discardPile.Count - 1; i++)
            {
                cards.Add(discardPile[i]);
            }

            discardPile.Clear();
            discardPile.Add(topCard);

            Shuffle();
        }
    }
}