using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class VerticalBoardWord : BoardWord
    {
        protected override BoardWordDirectionEnum Direction => BoardWordDirectionEnum.Vertical;
        public VerticalBoardWord(List<BoardTile> boardTiles = null) : base(boardTiles)
        {
        }
    }
}