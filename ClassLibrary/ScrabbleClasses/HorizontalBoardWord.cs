using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class HorizontalBoardWord : BoardWord
    {
        protected override BoardWordDirectionEnum Direction => BoardWordDirectionEnum.Horizontal;
        public HorizontalBoardWord(List<BoardTile> boardTiles = null) : base(boardTiles)
        {
        }
    }
}
