using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using DawgSharp;

namespace ClassLibrary
{
    public class Board
    {
        public BoardTile[][] Tiles { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public Board(int rowCount, int columnCount)
        {
            if (rowCount < 1) throw new Exception(ExceptionMessages.BoardMustHaveAtLeastOneRow);
            if (columnCount < 1) throw new Exception(ExceptionMessages.BoardMustHaveAtLeastOneColumn);

            BoardTile[][] rowsAndColumns = new BoardTile[rowCount][];
            for (int i = 0; i < rowsAndColumns.Length; i++)
            {
                rowsAndColumns[i] = new BoardTile[columnCount];
                for (int j = 0; j < rowsAndColumns[i].Length; j++) rowsAndColumns[i][j] = new BoardTile(x: i + 1, y: j + 1);
            }
            Tiles = rowsAndColumns;
            RowCount = rowCount;
            ColumnCount = columnCount;
        }

        public CharTile PlaceCharTile(int X, int Y, CharTile c)
        {
            if (!CoordinatesExist(X, Y)) return null;
            BoardTile boardTile = GetBoardTileAtCoordinates(X, Y);
            boardTile.PlaceCharTile(c);
            return c;
        }

        public CharTile PlaceCharTile(int X, int Y, char c)
        {
            return PlaceCharTile(X, Y, new CharTile(c));
        }

        public void RemoveCharTile(int X, int Y)
        {
            PlaceCharTile(X, Y, null);
        }

        public BoardTile GetBoardTileAtCoordinates(int X, int Y)
        {
            return !CoordinatesExist(X, Y) ? null : Tiles[X - 1][Y - 1];
        }

        public bool CoordinatesExist(int X, int Y)
        {
            return !(X < 1 || X > RowCount || Y < 1 || Y > ColumnCount);
        }
    }
}
