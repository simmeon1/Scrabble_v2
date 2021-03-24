using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class HorizontalBoardWord : BoardWord
    {
        public HorizontalBoardWord(List<BoardTile> boardTiles = null) : base(boardTiles)
        {
        }

        protected override int GetCoordinateThatIsConsistentInTheWord(BoardTile boardTile)
        {
            return boardTile.X;
        }

        protected override int GetCoordinateThatIsIncrementalInTheWord(BoardTile boardTile)
        {
            return boardTile.Y;
        }
    }
}
