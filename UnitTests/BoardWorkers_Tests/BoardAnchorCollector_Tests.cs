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
    public class BoardAnchorCollector_Tests
    {
        [TestMethod]
        public void GetAnchors_SetCharactersOnBoardTiles_AssertThatAnchorCountIsCorrectAfterEachPlacement()
        {
            Board board = new(rowCount: 5, columnCount: 5);

            BoardAnchorCollector boardAnchorCollector = new BoardAnchorCollector();

            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 0);

            board.PlaceCharTile(1, 1, 'H');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 2);
            board.RemoveCharTile(1, 1);

            board.PlaceCharTile(1, 2, 'H');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 3);
            board.RemoveCharTile(1, 2);

            board.PlaceCharTile(2, 2, 'H');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 4);

            board.PlaceCharTile(2, 3, 'E');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 6);

            board.PlaceCharTile(3, 3, 'E');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 7);

            board.PlaceCharTile(1, 3, 'O');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 7);

            board.PlaceCharTile(4, 3, 'O');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 9);

            board.PlaceCharTile(4, 2, 'Y');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 10);

            board.PlaceCharTile(4, 1, 'D');
            Assert.IsTrue(boardAnchorCollector.GetAnchors(board).Count == 11);
        }
    }
}
