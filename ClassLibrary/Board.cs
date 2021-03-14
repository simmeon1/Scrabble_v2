using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace ClassLibrary
{
    public class Board
    {
        private BoardTile[][] Tiles { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        private bool Transposed { get; set; }

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

        public CharTile SetCharTile(int X, int Y, CharTile c)
        {
            if (!CoordinatesExist(X, Y)) return null;
            BoardTile boardTile = GetBoardTileAtCoordinates(X, Y);
            boardTile.CharTile = c;
            return c;
        }

        public CharTile SetCharTile(int X, int Y, char c)
        {
            return SetCharTile(X, Y, new CharTile(c));
        }

        public BoardTile GetBoardTileAtCoordinates(int X, int Y)
        {
            return !CoordinatesExist(X, Y) ? null : Tiles[X - 1][Y - 1];
        }

        private bool CoordinatesExist(int X, int Y)
        {
            return !(X < 1 || X > RowCount || Y < 1 || Y > ColumnCount);
        }

        public string PrintBoard()
        {
            HashSet<string> anchorIds = GetAnchors().Select(a => a.Guid).ToHashSet();

            const string delimiter = "-------------------------------";
            StringBuilder sb = new StringBuilder(delimiter);
            foreach (BoardTile[] rows in Tiles)
            {
                if (sb.Length != 0) sb.Append('\n');
                foreach (BoardTile tile in rows) sb.Append(anchorIds.Contains(tile.Guid) ? "[=]" : $"[{tile.PrintChar()}]");
            }
            sb.Append('\n');
            sb.Append(delimiter);
            return sb.ToString();
        }

        public HorizontalBoardWord GetHorizontalWordTilesAtCoordinates(int X, int Y)
        {
            if (!CoordinatesExist(X, Y)) return null;
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

        public void Transpose()
        {
            BoardTile[][] newBoard = new BoardTile[ColumnCount][];
            for (int y = 0; y < ColumnCount; y++)
            {
                newBoard[y] = new BoardTile[RowCount];
                for (int x = 0; x < RowCount; x++)
                {
                    BoardTile boardTile = Tiles[x][y];
                    newBoard[y][x] = boardTile;
                    int temp = boardTile.X;
                    boardTile.X = boardTile.Y;
                    boardTile.Y = temp;
                }
            }

            int temp2 = RowCount;
            RowCount = ColumnCount;
            ColumnCount = temp2;
            Tiles = newBoard;
            Transposed = !Transposed;
        }

        public VerticalBoardWord GetVerticalWordTilesAtCoordinates(int X, int Y)
        {
            if (!CoordinatesExist(X, Y)) return null;
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

        public AnchorTileCollection GetAnchors()
        {
            return new AnchorCollector().GetAnchors(this);
        }

        private class AnchorCollector
        {
            private List<AnchorTile> Anchors { get; set; }
            private HashSet<string> AddedAnchors { get; set; }
            private Board Board { get; set; }

            public AnchorTileCollection GetAnchors(Board board)
            {
                Board = board;
                Anchors = new List<AnchorTile>();
                AddedAnchors = new HashSet<string>();

                for (int X = 1; X <= Board.RowCount; X++)
                {
                    for (int Y = 1; Y <= Board.ColumnCount; Y++)
                    {
                        if (Board.GetBoardTileAtCoordinates(X, Y).CharTile == null) continue;
                        AnalyseBoardTileAtCoordinatesAndAddIfItIsAnAnchor(X - 1, Y);
                        AnalyseBoardTileAtCoordinatesAndAddIfItIsAnAnchor(X + 1, Y);
                        AnalyseBoardTileAtCoordinatesAndAddIfItIsAnAnchor(X, Y - 1);
                        AnalyseBoardTileAtCoordinatesAndAddIfItIsAnAnchor(X, Y + 1);
                    }
                }
                return new AnchorTileCollection(Anchors);
            }

            private void AnalyseBoardTileAtCoordinatesAndAddIfItIsAnAnchor(int X, int Y)
            {
                BoardTile boardTile = Board.GetBoardTileAtCoordinates(X, Y);
                if (boardTile == null || boardTile.CharTile != null) return;
                AnchorTile anchorTile = new(boardTile);
                if (AddedAnchors.Contains(anchorTile.Guid)) return;
                Anchors.Add(anchorTile);
                AddedAnchors.Add(anchorTile.Guid);
            }
        }
    }
}
