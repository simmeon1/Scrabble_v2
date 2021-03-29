using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ClassLibrary
{
    [DebuggerDisplay("{PrintBoard()}")]
    public class BoardWordRetriever
    {
        public Board Board { get; set; }
        public BoardWordRetriever(Board board)
        {
            Board = board;
        }


        public HorizontalBoardWord GetHorizontalWordTilesAtCoordinates(int X, int Y)
        {
            if (!Board.CoordinatesExist(X, Y)) return null;
            List<BoardTile> boardTilesWithCharTiles = new();

            if (Board.GetBoardTileAtCoordinates(X, Y).CharTile == null) return new HorizontalBoardWord(boardTilesWithCharTiles);

            int i = Y;
            while (i > 1)
            {
                BoardTile boardTile = Board.GetBoardTileAtCoordinates(X, i - 1);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= Board.ColumnCount - Y)
            {
                BoardTile boardTile = Board.GetBoardTileAtCoordinates(X, Y + i);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new HorizontalBoardWord(boardTilesWithCharTiles);
        }

        public VerticalBoardWord GetVerticalWordTilesAtCoordinates(int X, int Y)
        {
            if (!Board.CoordinatesExist(X, Y)) return null;
            List<BoardTile> boardTilesWithCharTiles = new List<BoardTile>();

            if (Board.GetBoardTileAtCoordinates(X, Y).CharTile == null) return new VerticalBoardWord(boardTilesWithCharTiles);

            int i = X;
            while (i > 1)
            {
                BoardTile boardTile = Board.GetBoardTileAtCoordinates(i - 1, Y);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles = boardTilesWithCharTiles.Prepend(boardTile).ToList();
                i--;
            }

            i = 0;
            while (i <= Board.RowCount - X)
            {
                BoardTile boardTile = Board.GetBoardTileAtCoordinates(X + i, Y);
                if (boardTile.CharTile == null) break;
                boardTilesWithCharTiles.Add(boardTile);
                i++;
            }
            return new VerticalBoardWord(boardTilesWithCharTiles);
        }

        public bool BoardTileIsVerticallyConnectedToCharTiles(int X, int Y)
        {
            BoardTile boardTile = Board.GetBoardTileAtCoordinates(X, Y);
            if (boardTile == null) return false;
            if (boardTile.CharTile != null) return true;

            BoardTile upperBoardTile = Board.GetBoardTileAtCoordinates(X - 1, Y);
            if (upperBoardTile != null && upperBoardTile.CharTile != null) return true;

            BoardTile lowerBoardTile = Board.GetBoardTileAtCoordinates(X + 1, Y);
            if (lowerBoardTile != null && lowerBoardTile.CharTile != null) return true;

            return false;
        }
    }
}