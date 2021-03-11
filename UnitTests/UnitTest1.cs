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
            Board board = new Board(rowCount: 7, columnCount: 7);

            board.PlaceCharTile(rowIndex: 1, columnIndex: 2, c: new CharTile('h', 10));
            board.PlaceCharTile(rowIndex: 2, columnIndex: 2, c: new CharTile('e', 10));
            board.PlaceCharTile(rowIndex: 3, columnIndex: 2, c: new CharTile('l', 10));
            board.PlaceCharTile(rowIndex: 4, columnIndex: 2, c: new CharTile('l', 10));
            board.PlaceCharTile(rowIndex: 5, columnIndex: 2, c: new CharTile('o', 10));

            board.PlaceCharTile(rowIndex: 1, columnIndex: 2, c: new CharTile('h', 10));
            board.PlaceCharTile(rowIndex: 1, columnIndex: 3, c: new CharTile('e', 10));
            board.PlaceCharTile(rowIndex: 1, columnIndex: 4, c: new CharTile('l', 10));
            board.PlaceCharTile(rowIndex: 1, columnIndex: 5, c: new CharTile('l', 10));
            board.PlaceCharTile(rowIndex: 1, columnIndex: 6, c: new CharTile('o', 10));

            string result = board.PrintBoard();
            Debug.WriteLine(result);
            File.WriteAllText("testResults.txt", result);
            /*
             [ ][ ][ ][ ][ ][ ][ ][ ][ ][ ]
             [ ][ ][A][ ][ ][ ][ ][ ][ ][ ]
             [ ][ ][ ][ ][ ][ ][ ][ ][ ][ ]
             [ ][ ][ ][ ][ ][ ][ ][ ][ ][ ]
             [ ][ ][ ][ ][ ][ ][ ][ ][ ][ ]
             */
        }
    }
}
