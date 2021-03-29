using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BoardTileVerticalWordConnectionChecker_Tests
    {
        [TestMethod]
        public void BoardTileIsVerticallyPartOfAWord_TestOnNonEmptyBoardTile_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, c: 'H');

            BoardTileVerticalWordConnectionChecker verticalChecker = new(board);
            Assert.IsTrue(verticalChecker.BoardTileIsVerticallyPartOfAWord(x, y) == true);
        }

        [TestMethod]
        public void BoardTileIsVerticallyPartOfAWord_TestOnEmptyBoardTile_NoNeighboringVerticalCharTiles_ResultIsFalse()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);

            BoardTileVerticalWordConnectionChecker verticalChecker = new(board);
            Assert.IsTrue(verticalChecker.BoardTileIsVerticallyPartOfAWord(x, y) == false);
        }

        [TestMethod]
        public void BoardTileIsVerticallyPartOfAWord_TestOnEmptyBoardTile_HasUpperCharTileNeighbor_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);
            board.PlaceCharTile(X: 1, Y: 1, 'H');

            BoardTileVerticalWordConnectionChecker verticalChecker = new(board);
            Assert.IsTrue(verticalChecker.BoardTileIsVerticallyPartOfAWord(x, y) == true);
        }

        [TestMethod]
        public void BoardTileIsVerticallyPartOfAWord_TestOnEmptyBoardTile_HasLowerCharTileNeighbor_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);
            board.PlaceCharTile(X: 3, Y: 1, 'H');

            BoardTileVerticalWordConnectionChecker verticalChecker = new(board);
            Assert.IsTrue(verticalChecker.BoardTileIsVerticallyPartOfAWord(x, y) == true);
        }
        
        [TestMethod]
        public void BoardTileIsVerticallyPartOfAWord_TestOnEmptyBoardTile_HasUpperAndLowerCharTileNeighbor_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);
            board.PlaceCharTile(X: 3, Y: 1, 'H');
            board.PlaceCharTile(X: 1, Y: 1, 'H');

            BoardTileVerticalWordConnectionChecker verticalChecker = new(board);
            Assert.IsTrue(verticalChecker.BoardTileIsVerticallyPartOfAWord(x, y) == true);
        }
        
        [TestMethod]
        public void BoardTileIsVerticallyPartOfAWord_TestOnEmptyBoardTile_OnlyOneRowAndColumn_ResultIsFalse()
        {
            int x = 1;
            int y = 1;
            Board board = new(rowCount: 1, columnCount: 1);

            BoardTileVerticalWordConnectionChecker verticalChecker = new(board);
            Assert.IsTrue(verticalChecker.BoardTileIsVerticallyPartOfAWord(x, y) == false);
        }
    }
}
