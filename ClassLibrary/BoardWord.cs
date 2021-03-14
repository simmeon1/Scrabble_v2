using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClassLibrary
{
    public abstract class BoardWord : BoardTileCollection
    {
        protected abstract int GetCoordinateThatIsConsistentInTheWord(BoardTile boardTile);
        protected abstract int GetCoordinateThatIsIncrementalInTheWord(BoardTile boardTile);
        protected BoardWord(List<BoardTile> boardTiles = null) : base(boardTiles)
        {
        }

        public bool BoardTilesAreConnected()
        {
            HashSet<int> consistentCoordinatesFound = new HashSet<int>();
            List<int> incrementalCoordinatesFound = new List<int>();

            List<BoardTile> orderedBoardTiles = BoardTiles.OrderBy(bt => GetCoordinateThatIsConsistentInTheWord(bt)).ThenBy(bt => GetCoordinateThatIsIncrementalInTheWord(bt)).ToList();
            foreach (BoardTile bt in orderedBoardTiles)
            {
                consistentCoordinatesFound.Add(GetCoordinateThatIsConsistentInTheWord(bt));
                incrementalCoordinatesFound.Add(GetCoordinateThatIsIncrementalInTheWord(bt));
            }

            if (consistentCoordinatesFound.Count != 1) return false;

            incrementalCoordinatesFound = incrementalCoordinatesFound.OrderBy(c => c).ToList();

            int lastCoordinate = 0;
            foreach (int coordinate in incrementalCoordinatesFound)
            {
                if (lastCoordinate > 0 && coordinate - lastCoordinate != 1) return false;
                lastCoordinate = coordinate;
            }
            return true;
        }

        public string GetWord()
        {
            StringBuilder sb = new();
            foreach (BoardTile boardTile in BoardTiles) sb.Append(boardTile.PrintChar());
            return sb.ToString();
        }
    }
}