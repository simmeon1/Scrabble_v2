namespace ClassLibrary
{
    public interface IPlayerRack
    {
        bool ContainsCharTile(char edge);
        char TakeCharTile(char edge);
        void AddCharTile(char rackChar);
    }
}