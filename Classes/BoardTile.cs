using System;

namespace Classes
{
    public class BoardTile
    {
        private CharTile CharTile { get; set; }
        public BoardTile(CharTile charTile = null)
        {
            CharTile = charTile;
        }
        public string PrintChar()
        {
            if (CharTile == null) return " ";
            return $"{CharTile.Letter}";
        }

        public void AddCharTile(CharTile c)
        {
            CharTile = c;
        }
    }
}
