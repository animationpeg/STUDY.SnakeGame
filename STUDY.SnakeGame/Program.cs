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
            Coord gridDimensions = new Coord(50, 20);
            Coord snakePos = new Coord(10, 1);
            Coord applePos = RandApplePos(gridDimensions);
            int frameDelay = 50; // In milliseconds
            Direction movementDirection = Direction.Down;
            int score = 0;

            List<Coord> snakePosHistory = new List<Coord>();
            int tailLength = 1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Score: {score}");
                snakePos = snakePos.Move(movementDirection);

                // Create playing field and print to the console
                for (int y = 0; y < gridDimensions.Y; y++)
                {
                    for (int x = 0; x < gridDimensions.X; x++)
                    {
                        Coord currentCoord = new Coord(x, y);

                        if (snakePos.Equals(currentCoord) || snakePosHistory.Contains(currentCoord))
                            Console.Write("■");
                        else if (applePos.Equals(currentCoord))
                            Console.Write("a");
                        else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
                            Console.Write("#");
                        else
                            Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                // Check to see if snake eats the apple
                if (snakePos.Equals(applePos))
                {
                    tailLength++; // Grows tail
                    score++;
                    applePos = RandApplePos(gridDimensions); // Creates new apple in random location
                }
                // Check for game over conditions
                else if (snakePos.X == 0 || snakePos.Y == 0 ||
                    snakePos.X == gridDimensions.X - 1 || snakePos.Y == gridDimensions.Y - 1 ||
                    snakePosHistory.Contains(snakePos))
                {
                    score = 0;
                    tailLength = 1;
                    snakePos = new Coord(10, 1);
                    snakePosHistory.Clear();
                    movementDirection = Direction.Down;
                    continue;
                }


                // Record the path of the snake in a list
                snakePosHistory.Add(snakePos);

                if (snakePosHistory.Count > tailLength )
                    snakePosHistory.RemoveAt(0);

                // Collect player input to direct the snake up, down, left, right
                DateTime time = DateTime.Now;

                while ((DateTime.Now - time).Milliseconds < frameDelay)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKey key = Console.ReadKey(true).Key;
                        Direction newDirection = movementDirection;

                        switch (key)
                        {
                            case ConsoleKey.LeftArrow:
                                    newDirection = Direction.Left; break;
                            case ConsoleKey.RightArrow:
                                    newDirection = Direction.Right; break;
                            case ConsoleKey.UpArrow:
                                    newDirection = Direction.Up; break;
                            case ConsoleKey.DownArrow:
                                    newDirection = Direction.Down; break;
                        }
                        if (!movementDirection.IsOpposite(newDirection))
                            movementDirection = newDirection;
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
