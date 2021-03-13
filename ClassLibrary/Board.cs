using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClassLibrary
{
    public class Board
    {
        private BoardTile[][] Tiles { get; set; }
        public int RowCount { get; }
        public int ColumnCount { get; }

        public Board(int rowCount, int columnCount)
        {
            if (rowCount < 1) throw new Exception(ExceptionMessages.BoardMustHaveAtLeastOneRow);
            if (columnCount < 1) throw new Exception(ExceptionMessages.BoardMustHaveAtLeastOneColumn);

            BoardTile[][] rowsAndColumns = new BoardTile[rowCount][];
            for (int i = 0; i < rowsAndColumns.Length; i++)
            {
                rowsAndColumns[i] = new BoardTile[columnCount];
                for (int j = 0; j < rowsAndColumns[i].Length; j++) rowsAndColumns[i][j] = new BoardTile(X: i + 1, Y: j + 1);
            }
            Tiles = rowsAndColumns;
            RowCount = rowCount;
            ColumnCount = columnCount;
        }

        public void SetCharTile(int X, int Y, CharTile c)
        {
            ThrowExceptionIfProvidedXandYDoNotExist(X, Y);
            BoardTile boardTile = GetBoardTileAtCoordinates(X, Y);
            boardTile.CharTile = c;
        }

        public BoardTile GetBoardTileAtCoordinates(int X, int Y)
        {
            ThrowExceptionIfProvidedXandYDoNotExist(X, Y);
            return Tiles[X - 1][Y - 1];
        }

        private void ThrowExceptionIfProvidedXandYDoNotExist(int X, int Y)
        {
            if (X > RowCount) throw new Exception(ExceptionMessages.SpecifiedRowPositionIsNotAvailableInTheBoard);
            if (Y > ColumnCount) throw new Exception(ExceptionMessages.SpecifiedColumnPositionIsNotAvailableInTheBoard);
        }

        public string PrintBoard()
        {
            StringBuilder sb = new StringBuilder();
            foreach (BoardTile[] rows in Tiles)
            {
                if (sb.Length != 0) sb.Append('\n');
                foreach (BoardTile tile in rows) sb.Append($"[{tile.PrintChar()}]");
            }
            return sb.ToString();
        }

        public HorizontalBoardWord GetHorizontalWordTilesAtCoordinates(int X, int Y)
        {
            ThrowExceptionIfProvidedXandYDoNotExist(X, Y);
            List<BoardTile> boardTilesWithCharTiles = new List<BoardTile>();

            if (GetBoardTileAtCoordinates(X, Y).CharTile == null) return new HorizontalBoardWord(boardTilesWithCharTiles);

            int i = Y;
            while (i > 1)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(X, i - 1);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= ColumnCount - Y)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(X, Y + i);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new HorizontalBoardWord(boardTilesWithCharTiles);
        }

        public VerticalBoardWord GetVerticalWordTilesAtCoordinates(int X, int Y)
        {
            ThrowExceptionIfProvidedXandYDoNotExist(X, Y);
            List<BoardTile> boardTilesWithCharTiles = new List<BoardTile>();

            if (GetBoardTileAtCoordinates(X, Y).CharTile == null) return new VerticalBoardWord(boardTilesWithCharTiles);

            int i = X;
            while (i > 1)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(i - 1, Y);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= RowCount - X)
            {
                BoardTile boardTile = GetBoardTileAtCoordinates(X + i, Y);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new VerticalBoardWord(boardTilesWithCharTiles);
        }
    }
}
