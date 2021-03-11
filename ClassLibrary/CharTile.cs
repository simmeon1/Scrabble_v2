using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class CharTile
    {
        public char Letter { get; set; }
        private int Score { get; set; }
        public CharTile(char letter, int score)
        {
            if (letter < 65 || letter > 90) throw new ArgumentException("The letter can only be between A and Z.");
            if (score < 1) throw new ArgumentException("The score must be greater than 0.");
            Letter = letter;
            Score = score;
        }

    }
}
