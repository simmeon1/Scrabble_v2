using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using DawgSharp;

namespace ClassLibrary
{
    [DebuggerDisplay("{PrintBoard()}")]
    public class Board
    {
        public const string PrintBoard_Delimiter = "-------------------------------";
        public BoardTile[][] Tiles { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public Dawg<bool> Dawg { get; set; }
        private bool Transposed { get; set; }

        public Board(int rowCount, int columnCount, Dawg<bool> dawg = null)
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
            Dawg = dawg ?? Globals.EnglishDawg;
        }

        public int GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(BoardTile boardTile, BoardTileCollection boardAnchors)
        {
            int count = 0;
            int boardTileY = boardTile.Y;
            while (boardTileY > 0)
            {
                BoardTile boardTileToTheLeft = GetBoardTileAtCoordinates(boardTile.X, boardTileY - 1);
                if (boardTileToTheLeft == null || boardAnchors.Contains(boardTileToTheLeft)) break;
                count++;
                boardTileY--;
            }
            return count;
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
            BoardTileCollection anchors = GetAnchors();
            StringBuilder sb = new StringBuilder(PrintBoard_Delimiter);
            foreach (BoardTile[] rows in Tiles)
            {
                if (sb.Length != 0) sb.Append('\n');
                foreach (BoardTile tile in rows) sb.Append(anchors.Contains(tile) ? "[=]" : $"[{tile.PrintChar()}]");
            }
            sb.Append('\n');
            sb.Append(PrintBoard_Delimiter);
            return sb.ToString();
        }

        public HorizontalBoardWord GetHorizontalWordTilesAtCoordinates(int X, int Y)
        {
            if (!CoordinatesExist(X, Y)) return null;
            List<BoardTile> boardTilesWithCharTiles = new();

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
            Tiles = newBoard;
            int temp2 = RowCount;
            RowCount = ColumnCount;
            ColumnCount = temp2;
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

        public BoardTileCollection GetAnchors()
        {
            return new AnchorCollector().GetAnchors(this);
        }

        public Dictionary<BoardTile, HashSet<char>> GetCrossChecksForBoardTiles(BoardTileCollection boardTiles)
        {
            return new CrossCheckCollector().GetCrossChecksForBoardTiles(this, boardTiles);
        }

        public HashSet<char> GetCrossChecksForBoardTile(BoardTile boardTile)
        {
            if (boardTile == null) return null;
            BoardTileCollection boardTileCollection = new(new List<BoardTile>() { boardTile });
            Dictionary<BoardTile, HashSet<char>> crossChecksForBoardTileCollection = new CrossCheckCollector().GetCrossChecksForBoardTiles(this, boardTileCollection);
            return crossChecksForBoardTileCollection.ContainsKey(boardTile) ? crossChecksForBoardTileCollection[boardTile] : null;
        }

        public List<string> GetPossibleMoves(BoardTile anchor, List<char> playerRack)
        {
            return new MoveFinder(this).GetPossibleMoves(anchor, playerRack);
        }
    }
}
