using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class BoardWord : BoardTileCollection
    {
        public BoardWord(List<BoardTile> boardTiles = null) : base(boardTiles)
        {
        }

        public string GetWord()
        {
            StringBuilder sb = new StringBuilder();
            foreach (BoardTile boardTile in BoardTiles) sb.Append(boardTile.PrintChar());
            return sb.ToString();
        }
    }
}
