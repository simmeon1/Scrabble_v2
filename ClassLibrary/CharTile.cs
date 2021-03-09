using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class CharTile
    {
        public char Letter { get; private set; }
        private int Score { get; set; }
        public CharTile(char letter, int score)
        {
            Letter = letter.ToUpper();
            Score = score;
        }

    }
}
