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
    public class Board_Tests
    {
        [TestMethod]
        public void NewBoard_7RowsAnd10Coulmns_BoardContains7RowsAnd10Columns()
        {
            const int x = 7;
            const int y = 10;
            Board board = new(rowCount: x, columnCount: y);
            Assert.IsTrue(board.Tiles.Length == x);
            Assert.IsTrue(board.RowCount == x);
            Assert.IsTrue(board.Tiles[0].Length == y);
            Assert.IsTrue(board.ColumnCount == y);
        }

        [TestMethod]
        public void SetCharTileTest_SettingCharOnValidCoordinates_BoardTileIsPopulatedWithChar_ReturnedObjectIsThePlacedCharTile()
        {
            const int x = 1;
            const int y = 10;
            Board board = new(x, y);
            CharTile charTileToPlace = new('A');
            CharTile placedCharTile = board.PlaceCharTile(x, y, charTileToPlace);
            Assert.IsTrue(board.Tiles[x - 1][y - 1].CharTile.Letter == 'A');
            Assert.IsTrue(placedCharTile == charTileToPlace);
        }

        [TestMethod]
        public void SetCharTileTest_SettingCharOnInvalidCoordinates_BoardTileIsNotPlaced_ReturnedObjectIsNull()
        {
            const int x = 1;
            const int y = 10;
            Board board = new(x, y);
            CharTile charTileToPlace = new('A');
            CharTile placedCharTile = board.PlaceCharTile(99, 99, charTileToPlace);
            Assert.IsTrue(placedCharTile == null);
        }

        [TestMethod]
        public void GetBoardTileCoordinates_PassingValidCoordinates_ReturnedObjectAreNotNull()
        {
            Board board = new(rowCount: 2, columnCount: 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1) != null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2) != null);
        }

        [TestMethod]
        public void GetBoardTileCoordinates_PassingInvalidCoordinate_ReturnedObjectAreNull()
        {
            Board board = new(rowCount: 2, columnCount: 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(0, 1) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 0) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(0, 0) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(-1, -1) == null);
        }

        [TestMethod]
        public void GetBoardTileAtCoordinates_SettingLetterWOnX2Y3_CharacterOnBoardTileAtX2Y3IsW()
        {
            const int x = 2;
            const int y = 3;
            Board board = new(rowCount: x, columnCount: y);
            board.PlaceCharTile(X: x, Y: y, c: new CharTile('W'));
            Assert.IsTrue(board.GetBoardTileAtCoordinates(x, y).CharTile.Letter == 'W');
        }

        [TestMethod]
        public void GetBoardTileAtCoordinates_CreateBoardWith2RowsAnd2Columns_XsAndYsOfBoardTilesAreCorrect()
        {
            Board board = new(rowCount: 2, columnCount: 2);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1).X == 1);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1).Y == 1);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 2).X == 1);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 2).Y == 2);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 1).X == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 1).Y == 1);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2).X == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2).Y == 2);
        }

        [TestMethod]
        public void NewBoard_CreationWithBadRowCount_IncorrectRowNumberExceptionIsThrown()
        {
            Board board;
            SharedFunctions.InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(() => board = new Board(0, 1), ExceptionMessages.BoardMustHaveAtLeastOneRow);
        }

        [TestMethod]
        public void NewBoard_CreationWithBadColumnCount_IncorrectColumnNumberExceptionIsThrown()
        {
            Board board;
            SharedFunctions.InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(() => board = new Board(1, -1), ExceptionMessages.BoardMustHaveAtLeastOneColumn);
        }
    }
}
