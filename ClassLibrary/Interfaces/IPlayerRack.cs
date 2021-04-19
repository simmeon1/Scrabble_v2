namespace ClassLibrary.Interfaces
{
    public interface IPlayerRack
    {
        bool ContainsCharTile(char charTile);
        char TakeCharTile(char charTile);
        void AddCharTile(char charTile);
    }
}
