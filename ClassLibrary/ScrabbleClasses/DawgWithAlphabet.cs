using ClassLibrary.Interfaces;
using System.Collections.Generic;
using DawgSharp;

namespace ClassLibrary
{
    public class DawgWithAlphabet : IDawgWithAlphabet
    {
        private Dawg<bool> Dawg { get; set; }
        private char[] Alphabet { get; set; }
        
        public DawgWithAlphabet(Dawg<bool> dawg, char[] alphabet)
        {
            Dawg = dawg;
            Alphabet = alphabet;
        }

        public char[] GetAlphabet()
        {
            return Alphabet;
        }

        public bool IsWordValid(string word)
        {
            return Dawg[word];
        }

        public IEnumerable<string> GetWordsWithGivenPrefix(string prefix)
        {
            Queue<string> result = new();
            IEnumerable<KeyValuePair<string, bool>> wordsWithPrefix = Dawg.MatchPrefix(prefix);
            foreach (KeyValuePair<string, bool> wordWithPrefix in wordsWithPrefix) result.Enqueue(wordWithPrefix.Key);
            return result;
        }
    }
}
