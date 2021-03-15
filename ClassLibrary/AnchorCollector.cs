using System.Collections.Generic;

namespace ClassLibrary
{
    public class AnchorCollector
    {
        private List<BoardTile> Anchors { get; set; }
        private HashSet<BoardTile> AddedAnchors { get; set; }
        private Board Board { get; set; }

        public BoardTileCollection GetAnchors(Board board)
        {
            Board = board;
            Anchors = new List<BoardTile>();
            AddedAnchors = new HashSet<BoardTile>();

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
            return new BoardTileCollection(Anchors);
        }

        private void AnalyseBoardTileAtCoordinatesAndAddIfItIsAnAnchor(int X, int Y)
        {
            BoardTile tile = Board.GetBoardTileAtCoordinates(X, Y);
            if (tile == null || tile.CharTile != null || AddedAnchors.Contains(tile)) return;
            Anchors.Add(tile);
            AddedAnchors.Add(tile);
        }
    }
}
