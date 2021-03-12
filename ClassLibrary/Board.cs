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

        public void SetCharTile(int rowPosition, int columnPosition, CharTile c)
        {
            ThrowArgumentExceptionIfProvidedRowAndColumnPositionsAreNotOnTheBoard(rowPosition, columnPosition);
            BoardTile boardTile = GetBoardTileAtPosition(rowPosition, columnPosition);
            boardTile.CharTile = c;
        }

        private BoardTile GetBoardTileAtPosition(int rowPosition, int columnPosition)
        {
            ThrowArgumentExceptionIfProvidedRowAndColumnPositionsAreNotOnTheBoard(rowPosition, columnPosition);
            return Tiles[rowPosition - 1][columnPosition - 1];
        }

        private void ThrowArgumentExceptionIfProvidedRowAndColumnPositionsAreNotOnTheBoard(int rowPosition, int columnPosition)
        {
            if (rowPosition > NumberOfVerticalTiles) throw new ArgumentException("The specified row position is not available in the board.");
            if (columnPosition > NumberOfHorizontalTiles) throw new ArgumentException("The specified column position is not available in the board.");
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

        public BoardTileCollection GetHorizontalWordTilesAtCoordinates(int rowPosition, int columnPosition)
        {
            ThrowArgumentExceptionIfProvidedRowAndColumnPositionsAreNotOnTheBoard(rowPosition, columnPosition);
            List<BoardTile> boardTilesWithCharTiles = new List<BoardTile>();

            if (GetBoardTileAtPosition(rowPosition, columnPosition).CharTile == null) return new BoardTileCollection(boardTilesWithCharTiles);

            int i = columnPosition;
            while (i > 1)
            {
                BoardTile boardTile = GetBoardTileAtPosition(rowPosition, i - 1);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= NumberOfHorizontalTiles - columnPosition)
            {
                BoardTile boardTile = GetBoardTileAtPosition(rowPosition, columnPosition + i);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new BoardTileCollection(boardTilesWithCharTiles);
        }

        public BoardTileCollection GetVerticalWordTilesAtCoordinates(int rowPosition, int columnPosition)
        {
            ThrowArgumentExceptionIfProvidedRowAndColumnPositionsAreNotOnTheBoard(rowPosition, columnPosition);
            List<BoardTile> boardTilesWithCharTiles = new List<BoardTile>();

            if (GetBoardTileAtPosition(rowPosition, columnPosition).CharTile == null) return new BoardTileCollection(boardTilesWithCharTiles);

            int i = rowPosition;
            while (i > 1)
            {
                BoardTile boardTile = GetBoardTileAtPosition(i - 1, columnPosition);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= NumberOfVerticalTiles - rowPosition)
            {
                BoardTile boardTile = GetBoardTileAtPosition(rowPosition + i, columnPosition);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new BoardTileCollection(boardTilesWithCharTiles);
        }
    }
}
