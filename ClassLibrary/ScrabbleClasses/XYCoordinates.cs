using System.Diagnostics;

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
    }
}
