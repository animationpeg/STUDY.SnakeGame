using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.SnakeGame
{
    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    internal static class DirectionExtensions
    {
        public static bool IsOpposite(this Direction a, Direction b)
        {
            return
                (a == Direction.Left && b == Direction.Right) ||
                (a == Direction.Right && b == Direction.Left) ||
                (a == Direction.Up && b == Direction.Down) ||
                (a == Direction.Down && b == Direction.Up);
        }
    }
}
