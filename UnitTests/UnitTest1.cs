using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

            board.SetCharTile(rowPosition: 1, columnPosition: 2, c: new CharTile('H', 10));
            board.SetCharTile(rowPosition: 2, columnPosition: 2, c: new CharTile('E', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 2, c: new CharTile('L', 10));
            board.SetCharTile(rowPosition: 4, columnPosition: 2, c: new CharTile('L', 10));
            board.SetCharTile(rowPosition: 5, columnPosition: 2, c: new CharTile('O', 10));

            string result = board.PrintBoard();
            Debug.WriteLine(result);

            Assert.IsTrue(board.NumberOfVerticalTiles == 7);
            Assert.IsTrue(board.NumberOfHorizontalTiles == 10);
        }

        [TestMethod]
        public void BoardCreationArgumentException()
        {
            Board board;
            Assert.ThrowsException<ArgumentException>(() => board = new Board(0, 1));
            Assert.ThrowsException<ArgumentException>(() => board = new Board(1, -1));
            Assert.ThrowsException<ArgumentException>(() => board = new Board(-3, 0));
        }

        [TestMethod]
        public void CharPlaceException()
        {
            Board board = new Board(1, 10);
            CharTile c = new CharTile('A', 10);
            board.SetCharTile(1, 10, c);
            Assert.ThrowsException<ArgumentException>(() => board.SetCharTile(2, 5, c));
            Assert.ThrowsException<ArgumentException>(() => board.SetCharTile(1, 11, c));
        }

        [TestMethod]
        public void CharTileCreationExceptions()
        {
            CharTile c;
            c = new CharTile('A', 10);
            c = new CharTile('Z', 10);
            Assert.ThrowsException<ArgumentException>(() => c = new CharTile('a', 10));
            Assert.ThrowsException<ArgumentException>(() => c = new CharTile('A', 0));
        }

        [TestMethod]
        public void ReadHorizontalWordTest()
        {
            Board board = new Board(rowCount: 7, columnCount: 7);
            board.SetCharTile(rowPosition: 1, columnPosition: 2, c: new CharTile('H', 10));
            board.SetCharTile(rowPosition: 1, columnPosition: 3, c: new CharTile('E', 10));
            board.SetCharTile(rowPosition: 1, columnPosition: 4, c: new CharTile('L', 10));
            board.SetCharTile(rowPosition: 1, columnPosition: 5, c: new CharTile('L', 10));
            board.SetCharTile(rowPosition: 1, columnPosition: 6, c: new CharTile('O', 10));

            board.SetCharTile(rowPosition: 2, columnPosition: 2, c: new CharTile('F', 10));
            board.SetCharTile(rowPosition: 2, columnPosition: 3, c: new CharTile('R', 10));
            board.SetCharTile(rowPosition: 2, columnPosition: 4, c: new CharTile('I', 10));
            board.SetCharTile(rowPosition: 2, columnPosition: 5, c: new CharTile('E', 10));
            board.SetCharTile(rowPosition: 2, columnPosition: 6, c: new CharTile('N', 10));
            board.SetCharTile(rowPosition: 2, columnPosition: 7, c: new CharTile('D', 10));

            board.SetCharTile(rowPosition: 3, columnPosition: 1, c: new CharTile('B', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 2, c: new CharTile('Y', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 3, c: new CharTile('E', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 4, c: new CharTile('B', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 5, c: new CharTile('Y', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 6, c: new CharTile('E', 10));

            board.SetCharTile(rowPosition: 4, columnPosition: 4, c: new CharTile('M', 10));
            board.SetCharTile(rowPosition: 4, columnPosition: 5, c: new CharTile('A', 10));
            board.SetCharTile(rowPosition: 4, columnPosition: 6, c: new CharTile('T', 10));
            board.SetCharTile(rowPosition: 4, columnPosition: 7, c: new CharTile('E', 10));

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
            board.SetCharTile(rowPosition: 2, columnPosition: 1, c: new CharTile('H', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 1, c: new CharTile('E', 10));
            board.SetCharTile(rowPosition: 4, columnPosition: 1, c: new CharTile('L', 10));
            board.SetCharTile(rowPosition: 5, columnPosition: 1, c: new CharTile('L', 10));
            board.SetCharTile(rowPosition: 6, columnPosition: 1, c: new CharTile('O', 10));
                              
            board.SetCharTile(rowPosition: 2, columnPosition: 2, c: new CharTile('F', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 2, c: new CharTile('R', 10));
            board.SetCharTile(rowPosition: 4, columnPosition: 2, c: new CharTile('I', 10));
            board.SetCharTile(rowPosition: 5, columnPosition: 2, c: new CharTile('E', 10));
            board.SetCharTile(rowPosition: 6, columnPosition: 2, c: new CharTile('N', 10));
            board.SetCharTile(rowPosition: 7, columnPosition: 2, c: new CharTile('D', 10));

            board.SetCharTile(rowPosition: 1, columnPosition: 3, c: new CharTile('B', 10));
            board.SetCharTile(rowPosition: 2, columnPosition: 3, c: new CharTile('Y', 10));
            board.SetCharTile(rowPosition: 3, columnPosition: 3, c: new CharTile('E', 10));
            board.SetCharTile(rowPosition: 4, columnPosition: 3, c: new CharTile('B', 10));
            board.SetCharTile(rowPosition: 5, columnPosition: 3, c: new CharTile('Y', 10));
            board.SetCharTile(rowPosition: 6, columnPosition: 3, c: new CharTile('E', 10));
                              
            board.SetCharTile(rowPosition: 4, columnPosition: 4, c: new CharTile('M', 10));
            board.SetCharTile(rowPosition: 5, columnPosition: 4, c: new CharTile('A', 10));
            board.SetCharTile(rowPosition: 6, columnPosition: 4, c: new CharTile('T', 10));
            board.SetCharTile(rowPosition: 7, columnPosition: 4, c: new CharTile('E', 10));

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
    }
}
