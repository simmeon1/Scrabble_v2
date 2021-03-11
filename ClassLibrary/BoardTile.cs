using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibrary
{
    [DebuggerDisplay("{PrintChar()}")]
    public class BoardTile
    {
        public CharTile CharTile { get; set; }
        public BoardTile(CharTile charTile = null)
        {
            CharTile = charTile;
        }

        public string PrintChar()
        {
            if (CharTile == null) return " ";
            return $"{CharTile.Letter}";
        }
    }
}
