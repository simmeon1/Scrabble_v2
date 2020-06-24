using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class CharTile
    {
        public CharTile(char letter, int score)
        {
            Letter = letter.ToUpper();
            Score = score;
        }

        public char Letter { get; private set; }
        private int Score { get; set; }
    }
}
