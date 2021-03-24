using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ClassLibrary
{
    [DebuggerDisplay("{Letter}, {Score}")]
    public class CharTile
    {
        public char Letter { get; set; }
        private int Score { get; set; }
        public CharTile(char letter, int score = 1)
        {
            if (letter < 65 || letter > 90) throw new Exception(ExceptionMessages.LetterCanOnlyBeBetweenAAndZ);
            if (score < 1) throw new Exception(ExceptionMessages.ScoreMustBeGreaterThan0);
            Letter = letter;
            Score = score;
        }
    }
}
