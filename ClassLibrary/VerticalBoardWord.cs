using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class VerticalBoardWord : BoardWord
    {
        public VerticalBoardWord(List<BoardTile> boardTiles = null) : base(boardTiles)
        {
        }

        protected override int GetCoordinateThatIsConsistentInTheWord(BoardTile boardTile)
        {
            return boardTile.Y;
        }

        protected override int GetCoordinateThatIsIncrementalInTheWord(BoardTile boardTile)
        {
            return boardTile.X;
        }
    }
}