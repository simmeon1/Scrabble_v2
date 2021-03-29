using System.Collections.Generic;

namespace ClassLibrary
{
    public class BoardTileVerticalWordConnectionChecker
    {
        private Board Board { get; set; }
        public BoardTileVerticalWordConnectionChecker(Board board)
        {
            Board = board;
        }

        public bool BoardTileIsVerticallyPartOfAWord(int X, int Y)
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
