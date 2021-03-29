using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace ClassLibrary
{
    [DebuggerDisplay("{GetWord()}")]
    public abstract class BoardWord : BoardTileCollection
    {
        protected abstract BoardWordDirectionEnum Direction { get; }
        protected BoardWord(List<BoardTile> boardTiles = null) : base(boardTiles)
        {
        }

        public string GetWord()
        {
            StringBuilder sb = new();
            foreach (BoardTile boardTile in BoardTiles) sb.Append(boardTile.PrintChar());
            return sb.ToString();
        }

        protected enum BoardWordDirectionEnum
        {
            Unknown,
            Horizontal,
            Vertical
        }
    }
}