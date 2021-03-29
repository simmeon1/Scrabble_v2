using System.Diagnostics;

namespace ClassLibrary
{
    public class BoardNonAnchorTileCounter
    {
        public Board Board { get; set; }
        public BoardNonAnchorTileCounter(Board board)
        {
            Board = board;
        }

        public int GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(BoardTile boardTile, BoardTileCollection boardAnchors)
        {
            int count = 0;
            int boardTileY = boardTile.Y;
            while (boardTileY > 0)
            {
                BoardTile boardTileToTheLeft = Board.GetBoardTileAtCoordinates(boardTile.X, boardTileY - 1);
                if (boardTileToTheLeft == null || boardAnchors.Contains(boardTileToTheLeft)) break;
                count++;
                boardTileY--;
            }
            return count;
        }
    }
}