using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibrary
{
    [DebuggerDisplay("{PrintChar()}, X={X}, Y={Y}")]
    public class BoardTile
    {
        public CharTile CharTile { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Guid { get; }

        public BoardTile(int X, int Y, CharTile charTile = null, string guid = null)
        {
            CharTile = charTile;
            this.X = X;
            this.Y = Y;
            Guid = guid ?? System.Guid.NewGuid().ToString();
        }

        public char PrintChar()
        {
            return CharTile == null ? ' ' : CharTile.Letter;
        }
    }
}
