using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STUDY.SnakeGame
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false; // Hides cursor in the console
            Coord gridDimensions = new Coord(50, 20);
            Coord snakePos = new Coord(10, 5);
            Coord applePos = RandApplePos(gridDimensions);
            int frameDelay = 100; // In milliseconds
            Direction movementDirection = Direction.Down;
            int score = 0;
            Queue<Direction> inputQueue = new Queue<Direction>(); // Create queue for input buffer

            List<Coord> snakePosHistory = new List<Coord>();
            int tailLength = 1;
            int tailMaxLength = (gridDimensions.X - 2) * (gridDimensions.Y - 2); // max length of snake

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Score: {score}");
                snakePos = snakePos.Move(movementDirection);

                // Create playing field and print to the console
                for (int y = 0; y < gridDimensions.Y; y++)
                {
                    for (int x = 0; x < gridDimensions.X; x++)
                    {
                        Coord currentCoord = new Coord(x, y);

                        if (snakePos.Equals(currentCoord) || snakePosHistory.Contains(currentCoord))
                            Console.Write("■■");
                        else if (applePos.Equals(currentCoord))
                            Console.Write("aa");
                        else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
                            Console.Write("##");
                        else
                            Console.Write("  ");
                    }
                    Console.WriteLine();
                }

                // Check to see if snake eats the apple
                if (snakePos.Equals(applePos))
                {
                    tailLength++; // Grows tail
                    score++;
                    do // Creates new apple in random location, avoiding the snakes body
                    {
                    applePos = RandApplePos(gridDimensions);
                    } while (snakePosHistory.Contains(applePos) || snakePos.Equals(applePos));
                }
                // Check for game over conditions
                else if (snakePos.X == 0 || snakePos.Y == 0 ||
                    snakePos.X == gridDimensions.X - 1 || snakePos.Y == gridDimensions.Y - 1 ||
                    snakePosHistory.Contains(snakePos))
                {
                    score = 0;
                    tailLength = 1;
                    snakePos = new Coord(10, 5);
                    snakePosHistory.Clear();
                    movementDirection = Direction.Down;
                    continue;
                }
                // Check for game victory conditions
                else if (tailLength == tailMaxLength)
                {
                    Console.WriteLine($"Congratulations! You beat snake with a score of {score}!");
                    Console.ReadLine();
                    break;
                }


                // Record the path of the snake in a list
                snakePosHistory.Add(snakePos);

                if (snakePosHistory.Count > tailLength)
                    snakePosHistory.RemoveAt(0);

                // Collect player input to direct the snake up, down, left, right
                DateTime time = DateTime.Now;

                while ((DateTime.Now - time).Milliseconds < frameDelay)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKey key = Console.ReadKey(true).Key;
                        switch (key)
                        {
                            case ConsoleKey.LeftArrow:
                                if (inputQueue.Count < 5) inputQueue.Enqueue(Direction.Left); break;
                            case ConsoleKey.RightArrow:
                                if (inputQueue.Count < 5) inputQueue.Enqueue(Direction.Right); break;
                            case ConsoleKey.UpArrow:
                                if (inputQueue.Count < 5) inputQueue.Enqueue(Direction.Up); break;
                            case ConsoleKey.DownArrow:
                                if (inputQueue.Count < 5) inputQueue.Enqueue(Direction.Down); break;
                        }
                    }
                }
                // Process queue inputs if available
                if (inputQueue.Count > 0)
                {
                    Direction nextDirection = inputQueue.Dequeue();
                    if (!movementDirection.IsOpposite(nextDirection))
                    {
                        movementDirection = nextDirection;
                    }
                }
            }
        }
        private static readonly Random rand = new Random();
        static Coord RandApplePos(Coord gridDimensions)
        {
            return new Coord(
                rand.Next(1, gridDimensions.X - 1),
                rand.Next(1, gridDimensions.Y - 1));
        }
    }
}
