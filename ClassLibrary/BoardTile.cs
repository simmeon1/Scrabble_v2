using System;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibrary
{
    public class BoardTile
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        private CharTile CharTile { get; set; }
        public BoardTile(int rowIndex, int columnIndex, CharTile charTile = null)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            CharTile = charTile;
        }

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
