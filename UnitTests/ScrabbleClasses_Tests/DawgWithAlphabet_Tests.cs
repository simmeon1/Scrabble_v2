using ClassLibrary;
using DawgSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DawgWithAlphabet_Tests
    {
        [TestMethod]
        public void NewDawgWithAlphabet_UsingEnglishDawg_AlphabetIs26Characters()
        {
            DawgWithAlphabet dawgWithAlphabet = UnitTestGlobals.EnglishDawgWithAlphabet;
            char[] alphabet = dawgWithAlphabet.GetAlphabet();
            Assert.IsTrue(alphabet.Length == 26);
        }
    }
}
