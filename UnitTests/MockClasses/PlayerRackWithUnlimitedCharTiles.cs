using ClassLibrary.Interfaces;

namespace UnitTests.MockClasses
{
    public class PlayerRackWithUnlimitedCharTiles : IPlayerRack
    {
        public void AddCharTile(char rackChar)
        {
            return;
        }

        public bool ContainsCharTile(char edge)
        {
            return true;
        }

        public char TakeCharTile(char edge)
        {
            return edge;
        }
    }
}
