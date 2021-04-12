using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ClassLibrary
{
    [DebuggerDisplay("X={X}, Y={Y}")]
    public class XYCoordinates
    {
        public int X { get; }
        public int Y { get; }
        public XYCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool HasSameCoordinatesAs(XYCoordinates otherCoordinates)
        {
            return otherCoordinates != null && X == otherCoordinates.X && Y == otherCoordinates.Y;
        }
    }
}
