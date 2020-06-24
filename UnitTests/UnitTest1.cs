using Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_PrintBoard()
        {
            Board board = new Board(rowCount: 5, columnCount: 10);
            board.AddCharTile(rowIndex: 1, columnIndex: 2, c: new CharTile('a', 10));
            string result = board.PrintBoard();
            Console.WriteLine(result);
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
