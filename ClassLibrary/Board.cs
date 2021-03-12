using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClassLibrary
{
    public class Board
    {
        private BoardTile[][] Tiles { get; set; }
        public int NumberOfVerticalTiles { get; }
        public int NumberOfHorizontalTiles { get; }

        public Board(int rowCount, int columnCount)
        {
            if (rowCount < 1) throw new ArgumentException("The board must have at least one row.");
            if (columnCount < 1) throw new ArgumentException("The board must have at least one column.");

            BoardTile[][] rowsAndColumns = new BoardTile[rowCount][];
            for (int i = 0; i < rowsAndColumns.Length; i++)
            {
                rowsAndColumns[i] = new BoardTile[columnCount];
                for (int j = 0; j < rowsAndColumns[i].Length; j++) rowsAndColumns[i][j] = new BoardTile();
            }
            Tiles = rowsAndColumns;
            NumberOfVerticalTiles = rowCount;
            NumberOfHorizontalTiles = columnCount;
        }

        public void SetCharTile(int X, int Y, CharTile c)
        {
            ThrowArgumentExceptionIfProvidedXandYDoNotExist(X, Y);
            BoardTile boardTile = GetBoardTileAtCoordinates(X, Y);
            boardTile.CharTile = c;
        }

        private BoardTile GetBoardTileAtCoordinates(int X, int Y)
        {
            ThrowArgumentExceptionIfProvidedXandYDoNotExist(X, Y);
            return Tiles[X - 1][Y - 1];
        }

        private void ThrowArgumentExceptionIfProvidedXandYDoNotExist(int X, int Y)
        {
            if (X > NumberOfVerticalTiles) throw new ArgumentException("The specified row position is not available in the board.");
            if (Y > NumberOfHorizontalTiles) throw new ArgumentException("The specified column position is not available in the board.");
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

        public BoardWord GetHorizontalWordTilesAtCoordinates(int X, int Y)
        {
            ThrowArgumentExceptionIfProvidedXandYDoNotExist(X, Y);
            List<BoardTile> boardTilesWithCharTiles = new List<BoardTile>();

            if (GetBoardTileAtCoordinates(X, Y).CharTile == null) return new BoardWord(boardTilesWithCharTiles);

            int i = Y;
            while (i > 1)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(X, i - 1);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= NumberOfHorizontalTiles - Y)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(X, Y + i);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new BoardWord(boardTilesWithCharTiles);
        }

        public BoardWord GetVerticalWordTilesAtCoordinates(int X, int Y)
        {
            ThrowArgumentExceptionIfProvidedXandYDoNotExist(X, Y);
            List<BoardTile> boardTilesWithCharTiles = new List<BoardTile>();

            if (GetBoardTileAtCoordinates(X, Y).CharTile == null) return new BoardWord(boardTilesWithCharTiles);

            int i = X;
            while (i > 1)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(i - 1, Y);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= NumberOfVerticalTiles - X)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(X + i, Y);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new BoardWord(boardTilesWithCharTiles);
        }
    }
}
