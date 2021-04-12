using System.Collections.Generic;
using System.Diagnostics;

namespace ClassLibrary
{
    public class BoardTransposer
    {
        public bool BoardIsTransposed { get; private set; }
        public Dictionary<BoardTile, XYCoordinates> BoardTilesAndTheirOriginalCoordinates { get; }
        private Board Board { get; }
        public BoardTransposer(Board board)
        {
            Board = board;
            BoardIsTransposed = false;
            BoardTilesAndTheirOriginalCoordinates = new Dictionary<BoardTile, XYCoordinates>();
            for (int x = 0; x < Board.RowCount; x++)
            {
                for (int y = 0; y < Board.ColumnCount; y++)
                {
                    BoardTile boardTile = Board.Tiles[x][y];
                    BoardTilesAndTheirOriginalCoordinates.Add(boardTile, new XYCoordinates(boardTile.X, boardTile.Y));
                }
            }
        }


        public void TransposeBoard()
        {
            BoardTile[][] newBoard = new BoardTile[Board.ColumnCount][];
            for (int y = 0; y < Board.ColumnCount; y++)
            {
                newBoard[y] = new BoardTile[Board.RowCount];
                for (int x = 0; x < Board.RowCount; x++)
                {
                    BoardTile boardTile = Board.Tiles[x][y];
                    newBoard[y][x] = boardTile;
                    int temp = boardTile.X;
                    boardTile.X = boardTile.Y;
                    boardTile.Y = temp;
                }
            }
            Board.Tiles = newBoard;
            int temp2 = Board.RowCount;
            Board.RowCount = Board.ColumnCount;
            Board.ColumnCount = temp2;
            BoardIsTransposed = !BoardIsTransposed;
        }
    }
}