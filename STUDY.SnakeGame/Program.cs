using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.SnakeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Coord gridDimensions = new Coord(50, 20);
            Coord snakePos = new Coord(10, 0);

            // Create playing field
            for (int y = 0; y < gridDimensions.Y; y++)
            {
                for (int x = 0; x < gridDimensions.X; x++)
                {
                    if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
