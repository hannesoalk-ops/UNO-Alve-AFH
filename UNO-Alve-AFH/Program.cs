using System;
using System.Collections.Generic;

Random random = new Random();

List<string> deck = new List<string>();
List<string> discardPile = new List<string>();

List<string> playerNames = new List<string>();
List<List<string>> hands = new List<List<string>>();


// =========================
// PLAYERS
// =========================

int numberOfPlayers = 0;

while (numberOfPlayers < 2 || numberOfPlayers > 4)
{
    Console.Write("How many players? (2-4): ");

    if (!int.TryParse(Console.ReadLine(), out numberOfPlayers))
    {
        numberOfPlayers = 0;
    }

    if (numberOfPlayers < 2 || numberOfPlayers > 4)
    {
        Console.WriteLine("You must choose 2, 3 or 4 players.");
    }
}

for (int i = 0; i < numberOfPlayers; i++)
{
    Console.Write("Enter player " + (i + 1) + "'s name: ");
    playerNames.Add(Console.ReadLine());
    hands.Add(new List<string>());
}


// =========================
// CREATE DECK
// =========================

string[] colors = { "Red", "Yellow", "Green", "Blue" };

foreach (string color in colors)
{
    deck.Add(color + " 0");

    for (int number = 1; number <= 9; number++)
    {
        deck.Add(color + " " + number);
        deck.Add(color + " " + number);
    }

    deck.Add(color + " Skip");
    deck.Add(color + " Skip");

    deck.Add(color + " Reverse");
    deck.Add(color + " Reverse");

    deck.Add(color + " Draw Two");
    deck.Add(color + " Draw Two");
}

for (int i = 0; i < 4; i++)
{
    deck.Add("Wild");
    deck.Add("Wild Draw Four");
}


// =========================
// SHUFFLE
// =========================

ShuffleDeck();


// =========================
// DEAL 7 CARDS
// =========================

for (int player = 0; player < numberOfPlayers; player++)
{
    for (int card = 0; card < 7; card++)
    {
        DrawCard(player);
    }
}


// =========================
// FIRST CARD
// =========================

string firstCard = DrawFromDeck();

while (firstCard == "Wild Draw Four")
{
    deck.Add(firstCard);
    ShuffleDeck();
    firstCard = DrawFromDeck();
}

discardPile.Add(firstCard);

string currentColor;

if (firstCard == "Wild")
{
    currentColor = ChooseColor();
}
else
{
    currentColor = GetColor(firstCard);
}


// =========================
// STARTING PLAYER
// =========================

int currentPlayer = 0;
int direction = 1;


// First card effects

if (firstCard.Contains("Skip"))
{
    currentPlayer = NextPlayer(currentPlayer);
}

else if (firstCard.Contains("Reverse"))
{
    if (numberOfPlayers == 2)
    {
        currentPlayer = NextPlayer(currentPlayer);
    }
    else
    {
        direction = -1;
    }
}

else if (firstCard.Contains("Draw Two"))
{
    DrawCard(currentPlayer);
    DrawCard(currentPlayer);

    currentPlayer = NextPlayer(currentPlayer);
}


// =========================
// MAIN GAME
// =========================

while (true)
{
    Console.Clear();

    Console.WriteLine("================================");
    Console.WriteLine("              UNO");
    Console.WriteLine("================================");
    Console.WriteLine();

    Console.WriteLine("Card on table: " +
        discardPile[discardPile.Count - 1]);

    Console.WriteLine("Current color: " + currentColor);
    Console.WriteLine();

    for (int i = 0; i < numberOfPlayers; i++)
    {
        Console.WriteLine(
            playerNames[i] + ": " + hands[i].Count + " cards");
    }

    Console.WriteLine();
    Console.WriteLine("--------------------------------");
    Console.WriteLine();

    Console.WriteLine(
        playerNames[currentPlayer] + "'s turn!");

    Console.WriteLine();

    // Show cards
    for (int i = 0; i < hands[currentPlayer].Count; i++)
    {
        Console.WriteLine(
            (i + 1) + ". " + hands[currentPlayer][i]);
    }

    Console.WriteLine();
    Console.WriteLine("D = Draw a card");
    Console.WriteLine();

    Console.Write("Choose: ");
    string choice = Console.ReadLine();


    // =========================
    // DRAW
    // =========================

    if (choice.ToUpper() == "D")
    {
        string drawnCard = DrawCard(currentPlayer);

        Console.WriteLine();
        Console.WriteLine("You drew: " + drawnCard);

        // If the drawn card can be played,
        // the player may play it
        if (CanPlay(drawnCard, currentColor))
        {
            Console.Write("Do you want to play it? (Y/N): ");

            string answer = Console.ReadLine();

            if (answer.ToUpper() == "Y")
            {
                PlayCard(currentPlayer, hands[currentPlayer].Count - 1);

                if (drawnCard == "Wild" ||
                    drawnCard == "Wild Draw Four")
                {
                    currentColor = ChooseColor();
                }
                else
                {
                    currentColor = GetColor(drawnCard);
                }

                // Check win
                if (hands[currentPlayer].Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        playerNames[currentPlayer] + " WINS!");

                    break;
                }

                // UNO
                if (hands[currentPlayer].Count == 1)
                {
                    SayUno(currentPlayer);
                }

                // Special card
                if (drawnCard.Contains("Skip"))
                {
                    currentPlayer = NextPlayer(currentPlayer);
                    currentPlayer = NextPlayer(currentPlayer);
                }

                else if (drawnCard.Contains("Reverse"))
                {
                    if (numberOfPlayers == 2)
                    {
                        currentPlayer = NextPlayer(currentPlayer);
                        currentPlayer = NextPlayer(currentPlayer);
                    }
                    else
                    {
                        direction = direction * -1;
                        currentPlayer = NextPlayer(currentPlayer);
                    }
                }

                else if (drawnCard.Contains("Draw Two"))
                {
                    currentPlayer = NextPlayer(currentPlayer);

                    DrawCard(currentPlayer);
                    DrawCard(currentPlayer);

                    currentPlayer = NextPlayer(currentPlayer);
                }

                else if (drawnCard == "Wild Draw Four")
                {
                    currentPlayer = NextPlayer(currentPlayer);

                    DrawCard(currentPlayer);
                    DrawCard(currentPlayer);
                    DrawCard(currentPlayer);
                    DrawCard(currentPlayer);

                    currentPlayer = NextPlayer(currentPlayer);
                }

                else
                {
                    currentPlayer = NextPlayer(currentPlayer);
                }
            }
            else
            {
                currentPlayer = NextPlayer(currentPlayer);
            }
        }
        else
        {
            currentPlayer = NextPlayer(currentPlayer);
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter...");
        Console.ReadLine();

        continue;
    }


    // =========================
    // PLAY CARD
    // =========================

    if (!int.TryParse(choice, out int cardNumber))
    {
        Console.WriteLine("Invalid choice.");
        Console.ReadLine();
        continue;
    }

    cardNumber--;

    if (cardNumber < 0 ||
        cardNumber >= hands[currentPlayer].Count)
    {
        Console.WriteLine("That card does not exist.");
        Console.ReadLine();
        continue;
    }

    string selectedCard = hands[currentPlayer][cardNumber];


    // Check if card can be played

    if (!CanPlay(selectedCard, currentColor))
    {
        Console.WriteLine("You cannot play that card.");
        Console.ReadLine();
        continue;
    }


    // =========================
    // WILD DRAW FOUR RULE
    // =========================

    if (selectedCard == "Wild Draw Four")
    {
        bool hasCurrentColor = false;

        foreach (string card in hands[currentPlayer])
        {
            if (GetColor(card) == currentColor)
            {
                hasCurrentColor = true;
            }
        }

        if (hasCurrentColor)
        {
            Console.WriteLine();
            Console.WriteLine(
                "You cannot play Wild Draw Four.");
            Console.WriteLine(
                "You have a card of the current color.");

            Console.ReadLine();
            continue;
        }
    }


    // =========================
    // PLAY THE CARD
    // =========================

    PlayCard(currentPlayer, cardNumber);

    Console.WriteLine();
    Console.WriteLine(
        playerNames[currentPlayer] +
        " played " + selectedCard);


    // =========================
    // CHOOSE COLOR
    // =========================

    if (selectedCard == "Wild" ||
        selectedCard == "Wild Draw Four")
    {
        currentColor = ChooseColor();
    }
    else
    {
        currentColor = GetColor(selectedCard);
    }


    // =========================
    // WIN
    // =========================

    if (hands[currentPlayer].Count == 0)
    {
        Console.WriteLine();
        Console.WriteLine("================================");
        Console.WriteLine(
            playerNames[currentPlayer] + " WINS!");
        Console.WriteLine("================================");

        break;
    }


    // =========================
    // UNO
    // =========================

    if (hands[currentPlayer].Count == 1)
    {
        SayUno(currentPlayer);
    }


    // =========================
    // SPECIAL CARDS
    // =========================

    if (selectedCard.Contains("Skip"))
    {
        // Skip the next player
        currentPlayer = NextPlayer(currentPlayer);
        currentPlayer = NextPlayer(currentPlayer);
    }

    else if (selectedCard.Contains("Reverse"))
    {
        if (numberOfPlayers == 2)
        {
            // Reverse acts like Skip with 2 players
            currentPlayer = NextPlayer(currentPlayer);
            currentPlayer = NextPlayer(currentPlayer);
        }
        else
        {
            direction = direction * -1;
            currentPlayer = NextPlayer(currentPlayer);
        }
    }

    else if (selectedCard.Contains("Draw Two"))
    {
        // Next player draws 2 and loses turn
        currentPlayer = NextPlayer(currentPlayer);

        DrawCard(currentPlayer);
        DrawCard(currentPlayer);

        currentPlayer = NextPlayer(currentPlayer);
    }

    else if (selectedCard == "Wild Draw Four")
    {
        // Next player draws 4 and loses turn
        currentPlayer = NextPlayer(currentPlayer);

        DrawCard(currentPlayer);
        DrawCard(currentPlayer);
        DrawCard(currentPlayer);
        DrawCard(currentPlayer);

        currentPlayer = NextPlayer(currentPlayer);
    }

    else
    {
        // Normal card
        currentPlayer = NextPlayer(currentPlayer);
    }


    Console.WriteLine();
    Console.WriteLine("Press Enter for next turn.");
    Console.ReadLine();
}


// =========================
// FUNCTIONS
// =========================

string DrawCard(int player)
{
    if (deck.Count == 0)
    {
        RefillDeck();
    }

    string card = deck[0];

    deck.RemoveAt(0);
    hands[player].Add(card);

    return card;
}


string DrawFromDeck()
{
    if (deck.Count == 0)
    {
        RefillDeck();
    }

    string card = deck[0];

    deck.RemoveAt(0);

    return card;
}


void PlayCard(int player, int cardNumber)
{
    string card = hands[player][cardNumber];

    hands[player].RemoveAt(cardNumber);
    discardPile.Add(card);
}


string GetColor(string card)
{
    if (card.StartsWith("Red"))
        return "Red";

    if (card.StartsWith("Yellow"))
        return "Yellow";

    if (card.StartsWith("Green"))
        return "Green";

    if (card.StartsWith("Blue"))
        return "Blue";

    return "";
}


bool CanPlay(string card, string currentColor)
{
    // Wild cards can always be played
    if (card == "Wild" ||
        card == "Wild Draw Four")
    {
        return true;
    }

    // Same color
    if (GetColor(card) == currentColor)
    {
        return true;
    }

    string topCard =
        discardPile[discardPile.Count - 1];


    // Same number
    if (IsNumberCard(card) &&
        IsNumberCard(topCard))
    {
        if (GetNumber(card) == GetNumber(topCard))
        {
            return true;
        }
    }


    // Same action
    if (card.Contains("Skip") &&
        topCard.Contains("Skip"))
    {
        return true;
    }

    if (card.Contains("Reverse") &&
        topCard.Contains("Reverse"))
    {
        return true;
    }

    if (card.Contains("Draw Two") &&
        topCard.Contains("Draw Two"))
    {
        return true;
    }

    return false;
}


bool IsNumberCard(string card)
{
    string lastPart =
        card.Substring(card.LastIndexOf(" ") + 1);

    int number;

    return int.TryParse(lastPart, out number);
}


int GetNumber(string card)
{
    string lastPart =
        card.Substring(card.LastIndexOf(" ") + 1);

    return Convert.ToInt32(lastPart);
}


string ChooseColor()
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
            return "Red";

        if (choice == "2")
            return "Yellow";

        if (choice == "3")
            return "Green";

        if (choice == "4")
            return "Blue";

        Console.WriteLine("Please choose 1, 2, 3 or 4.");
    }
}


void SayUno(int player)
{
    Console.WriteLine();
    Console.WriteLine(
        playerNames[player] +
        " has one card left!");

    Console.Write("Type UNO: ");

    string answer = Console.ReadLine();

    if (answer.ToUpper() != "UNO")
    {
        Console.WriteLine();
        Console.WriteLine(
            "You forgot to say UNO!");

        Console.WriteLine("You draw 2 cards.");

        DrawCard(player);
        DrawCard(player);
    }
}


int NextPlayer(int player)
{
    player = player + direction;

    if (player >= numberOfPlayers)
    {
        player = 0;
    }

    if (player < 0)
    {
        player = numberOfPlayers - 1;
    }

    return player;
}


void ShuffleDeck()
{
    for (int i = deck.Count - 1; i > 0; i--)
    {
        int randomPlace = random.Next(i + 1);

        string temp = deck[i];

        deck[i] = deck[randomPlace];
        deck[randomPlace] = temp;
    }
}


void RefillDeck()
{
    if (discardPile.Count <= 1)
    {
        return;
    }

    string topCard =
        discardPile[discardPile.Count - 1];

    for (int i = 0; i < discardPile.Count - 1; i++)
    {
        deck.Add(discardPile[i]);
    }

    discardPile.Clear();
    discardPile.Add(topCard);

    ShuffleDeck();
}
