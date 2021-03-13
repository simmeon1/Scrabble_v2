﻿using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibrary
{
    [DebuggerDisplay("{PrintChar()}")]
    public class BoardTile
    {
        public CharTile CharTile { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public BoardTile(int X, int Y, CharTile charTile = null)
        {
            CharTile = charTile;
            this.X = X;
            this.Y = Y;
        }

        public char PrintChar()
        {
            if (CharTile == null) return ' ';
            return CharTile.Letter;
        }
    }
}
