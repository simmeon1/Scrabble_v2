using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ClassLibrary
{
    [DebuggerDisplay("{PrintChar()}, X={X}, Y={Y}")]
    public class BoardTile
    {
        public CharTile CharTile { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Guid Guid { get; } = Guid.NewGuid();
        public HashSet<char> CrossChecks { get; set; }
        public BoardTile(int x, int y, CharTile charTile = null)
        {
            CharTile = charTile;
            X = x;
            Y = y;
            CrossChecks = new HashSet<char>();
        }

        public char PrintChar()
        {
            if (CharTile == null) return ' ';
            return CharTile.Letter;
        }
    }
}
