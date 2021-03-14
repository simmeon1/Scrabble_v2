using ClassLibrary;
using DawgSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
        public void GetBoardTileAtCoordinatesTests()
        {
            Board board = new(rowCount: 2, columnCount: 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1) != null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2) != null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(0, 1) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 0) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(0, 0) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(-1, -1) == null);
        }

        [TestMethod]
        public void Test_CreateBoard_BoardTilesXsAndYs()
        {
            Board board = new(rowCount: 2, columnCount: 2);

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
        public void SetCharTileTest()
        {
            Board board = new(1, 10);
            CharTile c = new('A');
            Assert.IsTrue(board.SetCharTile(1, 11, c) == null);
            Assert.IsTrue(board.SetCharTile(1, 10, c).Equals(c));
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 10).CharTile.Equals(c));
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

            HorizontalBoardWord word0 = board.GetHorizontalWordTilesAtCoordinates(-1, -1);
            Assert.IsTrue(word0 == null);

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

            VerticalBoardWord word0 = board.GetVerticalWordTilesAtCoordinates(-1, -1);
            Assert.IsTrue(word0 == null);

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

            BoardTile char1 = new(1, 2, new CharTile('A'));
            BoardTile char2 = new(1, 3, new CharTile('B'));
            BoardTile char3 = new(1, 4, new CharTile('C'));
            List<BoardTile> charList = new() { char1, char2, char3 };
            horizontalWord = new HorizontalBoardWord(charList);
            Assert.IsTrue(horizontalWord.BoardTilesAreConnected());

            char1.X = 2;
            Assert.IsTrue(!horizontalWord.BoardTilesAreConnected());

            char1.X = 1;
            char1.Y = 1;
            Assert.IsTrue(!horizontalWord.BoardTilesAreConnected());

            char1.Y = 2;
            char3.Y = 5;
            Assert.IsTrue(!horizontalWord.BoardTilesAreConnected());
        }

        [TestMethod]
        public void TestVerticalBoardWord()
        {
            VerticalBoardWord VerticalWord;

            BoardTile char1 = new(2, 1, new CharTile('A'));
            BoardTile char2 = new(3, 1, new CharTile('B'));
            BoardTile char3 = new(4, 1, new CharTile('C'));
            List<BoardTile> charList = new() { char1, char2, char3 };

            VerticalWord = new VerticalBoardWord(charList);
            Assert.IsTrue(VerticalWord.BoardTilesAreConnected());

            char1.Y = 2;
            Assert.IsTrue(!VerticalWord.BoardTilesAreConnected());

            char1.Y = 1;
            char1.X = 1;
            Assert.IsTrue(!VerticalWord.BoardTilesAreConnected());

            char1.X = 2;
            char3.X = 5;
            Assert.IsTrue(!VerticalWord.BoardTilesAreConnected());
        }

        [TestMethod]
        public void TestGetAnchors()
        {
            Board board = new(rowCount: 5, columnCount: 5);
            Assert.IsTrue(board.GetAnchors().Count() == 0);

            board.SetCharTile(1, 1, 'H');
            Assert.IsTrue(board.GetAnchors().Count() == 2);
            board.SetCharTile(1, 1, null);

            board.SetCharTile(1, 2, 'H');
            Assert.IsTrue(board.GetAnchors().Count() == 3);
            board.SetCharTile(1, 2, null);

            board.SetCharTile(2, 2, 'H');
            Assert.IsTrue(board.GetAnchors().Count() == 4);

            board.SetCharTile(2, 3, 'E');
            Assert.IsTrue(board.GetAnchors().Count() == 6);

            board.SetCharTile(3, 3, 'E');
            Assert.IsTrue(board.GetAnchors().Count() == 7);

            board.SetCharTile(1, 3, 'O');
            Assert.IsTrue(board.GetAnchors().Count() == 7);

            board.SetCharTile(4, 3, 'O');
            Assert.IsTrue(board.GetAnchors().Count() == 9);

            board.SetCharTile(4, 2, 'Y');
            Assert.IsTrue(board.GetAnchors().Count() == 10);

            board.SetCharTile(4, 1, 'D');
            Assert.IsTrue(board.GetAnchors().Count() == 11);

            Debug.WriteLine(board.PrintBoard());
        }

        [TestMethod]
        [Ignore]
        public void GenerateDawgFromBoingFile()
        {
            string fileContents = File.ReadAllText("boing_crosschecks.txt");
            List<string> boingWords = Regex.Matches(fileContents, "\\w+").Select(m => m.Value).ToList();

            DawgBuilder<bool> dawgBuilder = new();
            foreach (string word in boingWords) dawgBuilder.Insert(word, true);

            Dawg<bool> dawg = dawgBuilder.BuildDawg(); // Computer is working.  Please wait ...

            using (FileStream file = File.Create("boingDAWG.bin")) dawg.SaveTo(file);

            //Now read the file back in and check if a particular word is in the dictionary:
            Dawg<bool> dawg2 = Dawg<bool>.Load(File.Open("boingDAWG.bin", FileMode.Open));
        }

        [TestMethod]
        public void TestDawgFile()
        {
            Dawg<bool> boingDawg = Globals.BoingDawg;
            Assert.IsTrue(boingDawg.Count() == 64);
        }

        [TestMethod]
        public void TestTransposition_1()
        {
            const int rowCount = 2;
            const int columnCount = 5;
            int[][] board = new int[rowCount][];
            board[0] = new int[columnCount];
            board[1] = new int[columnCount];

            board[0][0] = 0;
            board[0][1] = 1;
            board[0][2] = 2;
            board[0][3] = 3;
            board[0][4] = 4;
            board[1][0] = 5;
            board[1][1] = 6;
            board[1][2] = 7;
            board[1][3] = 8;
            board[1][4] = 9;

            int[][] newBoard = new int[columnCount][];
            for (int y = 0; y < columnCount; y++)
            {
                newBoard[y] = new int[rowCount];
                for (int x = 0; x < rowCount; x++)
                {
                    newBoard[y][x] = board[x][y];
                }
            }
        }

        [TestMethod]
        public void TestTransposition_2()
        {
            const int rowCount = 3;
            const int columnCount = 3;
            int[][] board = new int[rowCount][];
            board[0] = new int[columnCount];
            board[1] = new int[columnCount];
            board[2] = new int[columnCount];

            board[0][0] = 1;
            board[0][1] = 2;
            board[0][2] = 3;
            board[1][0] = 4;
            board[1][1] = 5;
            board[1][2] = 6;
            board[2][0] = 7;
            board[2][1] = 8;
            board[2][2] = 9;

            int[][] newBoard = new int[columnCount][];
            for (int y = 0; y < columnCount; y++)
            {
                newBoard[y] = new int[rowCount];
                for (int x = 0; x < rowCount; x++)
                {
                    newBoard[y][x] = board[x][y];
                }
            }
        }

        [TestMethod]
        public void Test_Transpose_Board()
        {
            Board board = new(rowCount: 2, columnCount: 3);
            board.SetCharTile(1, 1, 'A');
            board.SetCharTile(1, 2, 'B');
            board.SetCharTile(1, 3, 'C');
            board.SetCharTile(2, 1, 'D');
            board.SetCharTile(2, 2, 'E');
            board.SetCharTile(2, 3, 'F');

            Assert.IsTrue(board.RowCount == 2);
            Assert.IsTrue(board.ColumnCount == 3);
            BoardTile boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Guid guid = boardTile.Guid;
            Assert.IsTrue(boardTile.CharTile.Letter == 'B');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Debug.WriteLine(board.PrintBoard());
            
            board.Transpose();
            Assert.IsTrue(board.RowCount == 3);
            Assert.IsTrue(board.ColumnCount == 2);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'D');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 1).Guid.Equals(guid));
            Debug.WriteLine(board.PrintBoard());

            board.Transpose();
            Assert.IsTrue(board.RowCount == 2);
            Assert.IsTrue(board.ColumnCount == 3);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'B');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 2).Guid.Equals(guid));
            Debug.WriteLine(board.PrintBoard());
        }
    }
}
