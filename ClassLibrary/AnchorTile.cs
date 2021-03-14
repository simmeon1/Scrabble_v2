using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibrary
{
    //[DebuggerDisplay("{PrintChar()}, X={X}, Y={Y}")]
    public class AnchorTile : BoardTile
    {
        public HashSet<char> CrossChecks { get; set; } = new HashSet<char>();
        public AnchorTile(BoardTile boardTile) : base(boardTile.X, boardTile.Y, boardTile.CharTile, boardTile.Guid)
        {
        }
    }
}
