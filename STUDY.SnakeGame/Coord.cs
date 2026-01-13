using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.SnakeGame
{
    internal class Coord
    {
        private readonly int x;
        private readonly int y;

        public int X { get { return x; } }
        public int Y { get { return y; } }

        // Class Constructor
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        // .Equals override
        public override bool Equals(object obj)
        {
            if (obj is Coord other)
            {
                return x == other.x && y == other.y;
            }
            return false;
        }
        // .GetHashCode must also be overridden
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + x;
                hash = hash * 31 + y;
                return hash;
            }
        }

        public Coord Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Coord(x - 1, y);
                case Direction.Right:
                    return new Coord(x + 1, y);
                case Direction.Up:
                    return new Coord(x, y - 1);
                case Direction.Down:
                    return new Coord(x, y + 1);
                default:
                    return this; // No movement
            }
        }
    }
}
