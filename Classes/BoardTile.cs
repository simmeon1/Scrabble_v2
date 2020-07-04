using System;
using System.Security.Cryptography.X509Certificates;

namespace Classes
{
    public class BoardTile
    {
        public BoardTile(int rowIndex, int columnIndex, CharTile charTile = null)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            CharTile = charTile;
        }

        public int RowIndex { get; }
        public int ColumnIndex { get; }
        private CharTile CharTile { get; set; }

        public string PrintChar()
        {
            if (CharTile == null) return " ";
            return $"{CharTile.Letter}";
        }

        public void PlaceCharTile(CharTile c)
        {
            CharTile = c;
        }
    }
}
