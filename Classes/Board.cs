using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Board
    {
        private BoardTile[][] Tiles { get; set; }
        public Board(int rowCount, int columnCount)
        {
            BoardTile[][] rowsAndColumns = new BoardTile[rowCount][];
            for (int i = 0; i < rowsAndColumns.Length; i++)
            {
                rowsAndColumns[i] = new BoardTile[columnCount];
                for (int j = 0; j < rowsAndColumns[i].Length; j++) rowsAndColumns[i][j] = new BoardTile(rowIndex: i, columnIndex: j);
            }
            Tiles = rowsAndColumns;
        }

        public void PlaceCharTile(int rowIndex, int columnIndex, CharTile c)
        {
            Tiles[rowIndex][columnIndex].PlaceCharTile(c);
        }

        public string PrintBoard()
        {
            StringBuilder sb = new StringBuilder();
            foreach (BoardTile[] rows in Tiles)
            {
                if (sb.Length != 0) sb.Append("\n");
                foreach (BoardTile tile in rows) sb.Append($"[{tile.PrintChar()}]");
            }
            return sb.ToString();
        }
    }
}
