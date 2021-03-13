using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_PrintBoard()
        {
            Board board = new Board(rowCount: 7, columnCount: 10);

            board.SetCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.SetCharTile(X: 2, Y: 2, c: new CharTile('E'));
            board.SetCharTile(X: 3, Y: 2, c: new CharTile('L'));
            board.SetCharTile(X: 4, Y: 2, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 2, c: new CharTile('O'));

            string result = board.PrintBoard();
            Debug.WriteLine(result);

            Assert.IsTrue(board.RowCount == 7);
            Assert.IsTrue(board.ColumnCount == 10);
        }

        [TestMethod]
        public void Test_CreateBoard()
        {
            Board board = new Board(rowCount: 2, columnCount: 2);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1).X == 1);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1).Y == 1);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2).X == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2).Y == 2);
        }

        [TestMethod]
        public void BoardCreationException()
        {
            Board board;
            AssertThatCorrectExceptionIsThrown(() => board = new Board(0, 1), ExceptionMessages.BoardMustHaveAtLeastOneRow);
            AssertThatCorrectExceptionIsThrown(() => board = new Board(1, -1), ExceptionMessages.BoardMustHaveAtLeastOneColumn);
            AssertThatCorrectExceptionIsThrown(() => board = new Board(-3, 0), ExceptionMessages.BoardMustHaveAtLeastOneRow);
        }

        [TestMethod]
        public void CharPlaceException()
        {
            Board board = new Board(1, 10);
            CharTile c = new CharTile('A');
            board.SetCharTile(1, 10, c);

            AssertThatCorrectExceptionIsThrown(() => board.SetCharTile(2, 5, c), ExceptionMessages.SpecifiedRowPositionIsNotAvailableInTheBoard);
            AssertThatCorrectExceptionIsThrown(() => board.SetCharTile(1, 11, c), ExceptionMessages.SpecifiedColumnPositionIsNotAvailableInTheBoard);
        }

        private static void AssertThatCorrectExceptionIsThrown(Action action, string message)
        {
            Exception rowError = Assert.ThrowsException<Exception>(() => action.Invoke());
            Assert.IsTrue(rowError.Message.Equals(message));
        }

        [TestMethod]
        public void CharTileCreationExceptions()
        {
            CharTile c;
            c = new CharTile('A');
            c = new CharTile('Z');

            AssertThatCorrectExceptionIsThrown(() => c = new CharTile('a'), ExceptionMessages.LetterCanOnlyBeBetweenAAndZ);
            AssertThatCorrectExceptionIsThrown(() => c = new CharTile('A', 0), ExceptionMessages.ScoreMustBeGreaterThan0);
        }

        [TestMethod]
        public void ReadHorizontalWordTest()
        {
            Board board = new Board(rowCount: 7, columnCount: 7);
            board.SetCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.SetCharTile(X: 1, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 5, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 6, c: new CharTile('O'));

            board.SetCharTile(X: 2, Y: 2, c: new CharTile('F'));
            board.SetCharTile(X: 2, Y: 3, c: new CharTile('R'));
            board.SetCharTile(X: 2, Y: 4, c: new CharTile('I'));
            board.SetCharTile(X: 2, Y: 5, c: new CharTile('E'));
            board.SetCharTile(X: 2, Y: 6, c: new CharTile('N'));
            board.SetCharTile(X: 2, Y: 7, c: new CharTile('D'));

            board.SetCharTile(X: 3, Y: 1, c: new CharTile('B'));
            board.SetCharTile(X: 3, Y: 2, c: new CharTile('Y'));
            board.SetCharTile(X: 3, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 3, Y: 4, c: new CharTile('B'));
            board.SetCharTile(X: 3, Y: 5, c: new CharTile('Y'));
            board.SetCharTile(X: 3, Y: 6, c: new CharTile('E'));

            board.SetCharTile(X: 4, Y: 4, c: new CharTile('M'));
            board.SetCharTile(X: 4, Y: 5, c: new CharTile('A'));
            board.SetCharTile(X: 4, Y: 6, c: new CharTile('T'));
            board.SetCharTile(X: 4, Y: 7, c: new CharTile('E'));

            Debug.WriteLine(board.PrintBoard());

            string word1 = board.GetHorizontalWordTilesAtCoordinates(1, 3).GetWord();
            Assert.IsTrue(word1.Equals("HELLO"));

            string word2 = board.GetHorizontalWordTilesAtCoordinates(2, 5).GetWord();
            Assert.IsTrue(word2.Equals("FRIEND"));

            string word3 = board.GetHorizontalWordTilesAtCoordinates(3, 1).GetWord();
            Assert.IsTrue(word3.Equals("BYEBYE"));

            string word4 = board.GetHorizontalWordTilesAtCoordinates(4, 7).GetWord();
            Assert.IsTrue(word4.Equals("MATE"));
        }

        [TestMethod]
        public void ReadVerticalWordTest()
        {
            Board board = new Board(rowCount: 7, columnCount: 7);
            board.SetCharTile(X: 2, Y: 1, c: new CharTile('H'));
            board.SetCharTile(X: 3, Y: 1, c: new CharTile('E'));
            board.SetCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 6, Y: 1, c: new CharTile('O'));

            board.SetCharTile(X: 2, Y: 2, c: new CharTile('F'));
            board.SetCharTile(X: 3, Y: 2, c: new CharTile('R'));
            board.SetCharTile(X: 4, Y: 2, c: new CharTile('I'));
            board.SetCharTile(X: 5, Y: 2, c: new CharTile('E'));
            board.SetCharTile(X: 6, Y: 2, c: new CharTile('N'));
            board.SetCharTile(X: 7, Y: 2, c: new CharTile('D'));

            board.SetCharTile(X: 1, Y: 3, c: new CharTile('B'));
            board.SetCharTile(X: 2, Y: 3, c: new CharTile('Y'));
            board.SetCharTile(X: 3, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 4, Y: 3, c: new CharTile('B'));
            board.SetCharTile(X: 5, Y: 3, c: new CharTile('Y'));
            board.SetCharTile(X: 6, Y: 3, c: new CharTile('E'));

            board.SetCharTile(X: 4, Y: 4, c: new CharTile('M'));
            board.SetCharTile(X: 5, Y: 4, c: new CharTile('A'));
            board.SetCharTile(X: 6, Y: 4, c: new CharTile('T'));
            board.SetCharTile(X: 7, Y: 4, c: new CharTile('E'));

            Debug.WriteLine(board.PrintBoard());

            string word1 = board.GetVerticalWordTilesAtCoordinates(3, 1).GetWord();
            Assert.IsTrue(word1.Equals("HELLO"));

            string word2 = board.GetVerticalWordTilesAtCoordinates(5, 2).GetWord();
            Assert.IsTrue(word2.Equals("FRIEND"));

            string word3 = board.GetVerticalWordTilesAtCoordinates(1, 3).GetWord();
            Assert.IsTrue(word3.Equals("BYEBYE"));

            string word4 = board.GetVerticalWordTilesAtCoordinates(7, 4).GetWord();
            Assert.IsTrue(word4.Equals("MATE"));
        }

        [TestMethod]
        public void TestHorizontalBoardWord()
        {
            HorizontalBoardWord horizontalWord;
            AssertThatCorrectExceptionIsThrown(() => horizontalWord = new HorizontalBoardWord(), ExceptionMessages.ABoardWordMustConsistOf2OrMoreLetters);

            BoardTile char1 = new(1, 2, new CharTile('A'));
            BoardTile char2 = new(1, 3, new CharTile('B'));
            BoardTile char3 = new(1, 4, new CharTile('C'));
            List<BoardTile> charList = new() { char1, char2, char3 };
           
            horizontalWord = new HorizontalBoardWord(charList);

            char1.X = 2;
            AssertThatCorrectExceptionIsThrown(() => horizontalWord = new HorizontalBoardWord(charList), ExceptionMessages.BoardTilesAreNotHorizontallyConnected);

            char1.X = 1;
            char1.Y = 1;
            AssertThatCorrectExceptionIsThrown(() => horizontalWord = new HorizontalBoardWord(charList), ExceptionMessages.BoardTilesAreNotHorizontallyConnected);
            
            char1.Y = 2;
            char3.Y = 5;
            AssertThatCorrectExceptionIsThrown(() => horizontalWord = new HorizontalBoardWord(charList), ExceptionMessages.BoardTilesAreNotHorizontallyConnected);
        }

        [TestMethod]
        public void TestVerticalBoardWord()
        {
            VerticalBoardWord VerticalWord;
            AssertThatCorrectExceptionIsThrown(() => VerticalWord = new VerticalBoardWord(), ExceptionMessages.ABoardWordMustConsistOf2OrMoreLetters);

            BoardTile char1 = new(2, 1, new CharTile('A'));
            BoardTile char2 = new(3, 1, new CharTile('B'));
            BoardTile char3 = new(4, 1, new CharTile('C'));
            List<BoardTile> charList = new() { char1, char2, char3 };

            VerticalWord = new VerticalBoardWord(charList);

            char1.Y = 2;
            AssertThatCorrectExceptionIsThrown(() => VerticalWord = new VerticalBoardWord(charList), ExceptionMessages.BoardTilesAreNotVerticallyConnected);

            char1.Y = 1;
            char1.X = 1;
            AssertThatCorrectExceptionIsThrown(() => VerticalWord = new VerticalBoardWord(charList), ExceptionMessages.BoardTilesAreNotVerticallyConnected);

            char1.X = 2;
            char3.X = 5;
            AssertThatCorrectExceptionIsThrown(() => VerticalWord = new VerticalBoardWord(charList), ExceptionMessages.BoardTilesAreNotVerticallyConnected);
        }
    }
}
