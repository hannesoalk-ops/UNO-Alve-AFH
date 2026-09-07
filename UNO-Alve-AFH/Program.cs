using System;

Console.WriteLine("===== UNO =====");
Console.WriteLine();

Console.Write("How many players? (2-4): ");
int players = Convert.ToInt32(Console.ReadLine());

if (players == 2)
{
    Console.Write("Player 1 name: ");
    string player1 = Console.ReadLine();

    Console.Write("Player 2 name: ");
    string player2 = Console.ReadLine();

    int player1Cards = 7;
    int player2Cards = 7;

    Console.WriteLine();
    Console.WriteLine("Players:");
    Console.WriteLine(player1);
    Console.WriteLine(player2);

    Console.WriteLine();
    Console.WriteLine("The game starts!");

    Console.WriteLine();
    Console.WriteLine(player1 + "'s turn");
    Console.WriteLine("You have " + player1Cards + " cards.");
    Console.WriteLine("1. Play a card");
    Console.WriteLine("2. Draw a card");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        player1Cards = player1Cards - 1;
        Console.WriteLine("You played a card!");
    }
    else
    {
        player1Cards = player1Cards + 1;
        Console.WriteLine("You drew a card!");
    }

    Console.WriteLine();
    Console.WriteLine(player2 + "'s turn");
    Console.WriteLine("You have " + player2Cards + " cards.");
}
else if (players == 3)
{
    Console.Write("Player 1 name: ");
    string player1 = Console.ReadLine();

    Console.Write("Player 2 name: ");
    string player2 = Console.ReadLine();

    Console.Write("Player 3 name: ");
    string player3 = Console.ReadLine();

    int player1Cards = 7;
    int player2Cards = 7;
    int player3Cards = 7;

    Console.WriteLine();
    Console.WriteLine("Players:");
    Console.WriteLine(player1);
    Console.WriteLine(player2);
    Console.WriteLine(player3);

    Console.WriteLine();
    Console.WriteLine("The game starts!");

    Console.WriteLine();
    Console.WriteLine(player1 + "'s turn");
    Console.WriteLine("You have " + player1Cards + " cards.");
    Console.WriteLine("1. Play a card");
    Console.WriteLine("2. Draw a card");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        player1Cards = player1Cards - 1;
        Console.WriteLine("You played a card!");
    }
    else
    {
        player1Cards = player1Cards + 1;
        Console.WriteLine("You drew a card!");
    }

    Console.WriteLine();
    Console.WriteLine(player2 + "'s turn");
    Console.WriteLine("You have " + player2Cards + " cards.");

    Console.WriteLine();
    Console.WriteLine(player3 + "'s turn");
    Console.WriteLine("You have " + player3Cards + " cards.");
}
else if (players == 4)
{
    Console.Write("Player 1 name: ");
    string player1 = Console.ReadLine();

    Console.Write("Player 2 name: ");
    string player2 = Console.ReadLine();

    Console.Write("Player 3 name: ");
    string player3 = Console.ReadLine();

    Console.Write("Player 4 name: ");
    string player4 = Console.ReadLine();

    int player1Cards = 7;
    int player2Cards = 7;
    int player3Cards = 7;
    int player4Cards = 7;

    Console.WriteLine();
    Console.WriteLine("Players:");
    Console.WriteLine(player1);
    Console.WriteLine(player2);
    Console.WriteLine(player3);
    Console.WriteLine(player4);

    Console.WriteLine();
    Console.WriteLine("The game starts!");

    Console.WriteLine();
    Console.WriteLine(player1 + "'s turn");
    Console.WriteLine("You have " + player1Cards + " cards.");
    Console.WriteLine("1. Play a card");
    Console.WriteLine("2. Draw a card");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        player1Cards = player1Cards - 1;
        Console.WriteLine("You played a card!");
    }
    else
    {
        player1Cards = player1Cards + 1;
        Console.WriteLine("You drew a card!");
    }

    Console.WriteLine();
    Console.WriteLine(player2 + "'s turn");
    Console.WriteLine("You have " + player2Cards + " cards.");

    Console.WriteLine();
    Console.WriteLine(player3 + "'s turn");
    Console.WriteLine("You have " + player3Cards + " cards.");

    Console.WriteLine();
    Console.WriteLine(player4 + "'s turn");
    Console.WriteLine("You have " + player4Cards + " cards.");
}
else
{
    Console.WriteLine("You must have 2, 3 or 4 players!");
}

Console.WriteLine();
Console.WriteLine("Press Enter to close.");
Console.ReadLine();

