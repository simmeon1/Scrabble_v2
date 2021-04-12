using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CharTile_Tests
    {
        [TestMethod]
        public void NewCharTile_InitialisationWithLowerCaseChar_ThrowExceptionThatCharMustBeUppercase()
        {
            CharTile c;
            SharedFunctions.InvokeActionAndAssertThatCorrectExceptionMessageIsThrown (() => c = new CharTile('a'), ExceptionMessages.LetterCanOnlyBeBetweenAAndZ);
        }

        [TestMethod]
        public void NewCharTile_InitialisationWith0Score_ThrowExceptionThatScoreMustBeMoreThan0()
        {
            CharTile c;
            SharedFunctions.InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(() => c = new CharTile('A', 0), ExceptionMessages.ScoreMustBeGreaterThan0);
        }
    }
}
