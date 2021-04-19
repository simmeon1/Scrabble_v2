using System;
using System.Collections.Generic;

namespace ClassLibrary.Interfaces
{
    public interface IDawgWithAlphabet
    {
        char[] GetAlphabet();
        bool IsWordValid(string word);
        IEnumerable<string> GetWordsWithGivenPrefix(string prefix);
    }
}
