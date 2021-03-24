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
