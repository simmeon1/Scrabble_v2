using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class CharTile
    {
        private char letter;

        public char Letter
        {
            get
            {
                return letter;
            }
            set
            {
                letter = value.ToUpper();
            }
        }
        private int Score { get; set; }
        public CharTile(char letter, int score)
        {
            Letter = letter.ToUpper();
            Score = score;
        }

    }
}
