using System;
using System.Collections.Generic;

namespace UNO_Alve_AFH
{
    internal class Program
    {
        static void Main()
        {
            Deck deck = new Deck();
            Rules rules = new Rules();

            List<Player> players = new List<Player>();

            int numberOfPlayers = 0;

            while (numberOfPlayers < 2 || numberOfPlayers > 4)
            {
                Console.Write("How many players? (2-4): ");
                int.TryParse(Console.ReadLine(), out numberOfPlayers);

                if (numberOfPlayers < 2 || numberOfPlayers > 4)
                {
                    Console.WriteLine("Choose between 2 and 4 players.");
                }
            }

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Write("Enter name for player " + (i + 1) + ": ");
                string name = Console.ReadLine();

                players.Add(new Player(name));
            }

            // Give each player 7 cards
            for (int i = 0; i < players.Count; i++)
            {
                players[i].DrawCards(deck, 7);
            }

            // First card
            Card firstCard = deck.DrawCard();
            deck.AddToDiscard(firstCard);

            string currentColor = firstCard.color;

            if (firstCard.color == "")
            {
                currentColor = rules.ChooseColor();
            }

            int currentPlayer = 0;
            int direction = 1;

            bool gameRunning = true;

            while (gameRunning)
            {
                Console.Clear();

                Console.WriteLine("==============================");
                Console.WriteLine("             UNO");
                Console.WriteLine("==============================");

                Console.WriteLine();
                Console.WriteLine("Top card: " + deck.GetTopCard().GetName());
                Console.WriteLine("Current color: " + currentColor);

                Console.WriteLine();

                for (int i = 0; i < players.Count; i++)
                {
                    Console.WriteLine(players[i].name + ": " + players[i].hand.Count + " cards");
                }

                Console.WriteLine();
                Console.WriteLine(players[currentPlayer].name + "'s turn");
                Console.WriteLine();

                players[currentPlayer].ShowHand();

                Console.WriteLine();
                Console.WriteLine("D. Draw a card");
                Console.Write("Choose a card: ");

                string choice = Console.ReadLine();

                if (choice.ToUpper() == "D")
                {
                    Card drawnCard = deck.DrawCard();

                    Console.WriteLine();
                    Console.WriteLine("You drew: " + drawnCard.GetName());

                    if (rules.CanPlay(drawnCard, deck.GetTopCard(), currentColor))
                    {
                        Console.Write("Do you want to play it? (Y/N): ");
                        string answer = Console.ReadLine();

                        if (answer.ToUpper() == "Y")
                        {
                            deck.AddToDiscard(drawnCard);

                            if (drawnCard.type == "Wild" || drawnCard.type == "Wild Draw Four")
                            {
                                currentColor = rules.ChooseColor();
                            }
                            else
                            {
                                currentColor = drawnCard.color;
                            }

                            Console.WriteLine("You played " + drawnCard.GetName());

                            if (players[currentPlayer].hand.Count == 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine(players[currentPlayer].name + " wins!");
                                gameRunning = false;
                            }
                            else
                            {
                                currentPlayer = rules.GetNextPlayer(
                                    currentPlayer,
                                    direction,
                                    players.Count
                                );
                            }
                        }
                        else
                        {
                            players[currentPlayer].hand.Add(drawnCard);

                            currentPlayer = rules.GetNextPlayer(
                                currentPlayer,
                                direction,
                                players.Count
                            );
                        }
                    }
                    else
                    {
                        players[currentPlayer].hand.Add(drawnCard);

                        currentPlayer = rules.GetNextPlayer(
                            currentPlayer,
                            direction,
                            players.Count
                        );
                    }

                    if (gameRunning)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    int cardNumber;

                    if (int.TryParse(choice, out cardNumber))
                    {
                        cardNumber = cardNumber - 1;

                        if (cardNumber >= 0 && cardNumber < players[currentPlayer].hand.Count)
                        {
                            Card selectedCard = players[currentPlayer].hand[cardNumber];

                            if (rules.CanPlay(
                                selectedCard,
                                deck.GetTopCard(),
                                currentColor))
                            {
                                players[currentPlayer].hand.RemoveAt(cardNumber);
                                deck.AddToDiscard(selectedCard);

                                Console.WriteLine();
                                Console.WriteLine(
                                    players[currentPlayer].name +
                                    " played " +
                                    selectedCard.GetName()
                                );

                                if (selectedCard.type == "Wild" ||
                                    selectedCard.type == "Wild Draw Four")
                                {
                                    currentColor = rules.ChooseColor();
                                }
                                else
                                {
                                    currentColor = selectedCard.color;
                                }

                                // Skip
                                if (selectedCard.type == "Skip")
                                {
                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );

                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );
                                }

                                // Reverse
                                else if (selectedCard.type == "Reverse")
                                {
                                    direction = direction * -1;

                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );
                                }

                                // Draw Two
                                else if (selectedCard.type == "Draw Two")
                                {
                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );

                                    players[currentPlayer].DrawCards(deck, 2);

                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );
                                }

                                // Wild Draw Four
                                else if (selectedCard.type == "Wild Draw Four")
                                {
                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );

                                    players[currentPlayer].DrawCards(deck, 4);

                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );
                                }

                                else
                                {
                                    currentPlayer = rules.GetNextPlayer(
                                        currentPlayer,
                                        direction,
                                        players.Count
                                    );
                                }

                                if (players[currentPlayer].hand.Count == 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine(
                                        players[currentPlayer].name +
                                        " wins!"
                                    );

                                    gameRunning = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("You cannot play that card.");
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("That card does not exist.");
                        }

                        if (gameRunning)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Press Enter to continue...");
                            Console.ReadLine();
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Game over!");
        }
    }
}