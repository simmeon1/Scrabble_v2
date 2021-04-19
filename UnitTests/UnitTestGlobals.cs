using ClassLibrary;
using DawgSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnitTests
{
    public static class UnitTestGlobals
    {
        private static char[] englishAlphabet = GetEnglishCharactersArray();
        public static DawgWithAlphabet EnglishDawgWithAlphabet = new(LoadEnglishDawgFile(), englishAlphabet);
        public static DawgWithAlphabet BoingDawgWithAlphabet = new(LoadBoingDawgFile(), englishAlphabet);

        public static void InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(Action action, string message)
        {
            Exception rowError = Assert.ThrowsException<Exception>(() => action.Invoke());
            Assert.IsTrue(rowError.Message.Equals(message));
        }

        public static Dawg<bool> LoadEnglishDawgFile()
        {
            return Globals.LoadDawgFile("englishDawg.bin");
        }

        public static Dawg<bool> LoadBoingDawgFile()
        {
            return Globals.LoadDawgFile("boingDawg.bin");
        }

        private static char[] GetEnglishCharactersArray()
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }
    }
}
