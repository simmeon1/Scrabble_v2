using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class BoardTransposer_Tests
    {
        [TestMethod]
        public void Transpose_TransposingA2By3BoardTo3By2_AssertThatTheBoardTilesXsAndYsAndCharsAreAsExpected()
        {
            Board board = new(rowCount: 2, columnCount: 3);
            board.PlaceCharTile(1, 1, 'A');
            board.PlaceCharTile(1, 2, 'B');
            board.PlaceCharTile(1, 3, 'C');
            board.PlaceCharTile(2, 1, 'D');
            board.PlaceCharTile(2, 2, 'E');
            board.PlaceCharTile(2, 3, 'F');

            Assert.IsTrue(board.RowCount == 2);
            Assert.IsTrue(board.ColumnCount == 3);
            BoardTile boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Guid guid = boardTile.Guid;
            Assert.IsTrue(boardTile.CharTile.Letter == 'B');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);

            BoardTransposer.Transpose(board);
            Assert.IsTrue(board.RowCount == 3);
            Assert.IsTrue(board.ColumnCount == 2);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'D');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 1).Guid.Equals(guid));

            BoardTransposer.Transpose(board);
            Assert.IsTrue(board.RowCount == 2);
            Assert.IsTrue(board.ColumnCount == 3);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'B');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 2).Guid.Equals(guid));
        }

        [TestMethod]
        public void Transpose_TransposeATestBoard_AssertUntransposedXsAndUntransposedYsAreCorrect()
        {
            Board board = new(rowCount: 5, columnCount: 5);
            board.PlaceCharTile(2, 3, 'A');
            board.PlaceCharTile(2, 4, 'B');

            BoardTransposer.Transpose(board);

            BoardTile boardTileA = board.GetBoardTileAtCoordinates(3, 2);

            Assert.IsTrue(boardTileA.X == 3);
            Assert.IsTrue(boardTileA.UntransposedX == 2);
            Assert.IsTrue(boardTileA.Y == 2);
            Assert.IsTrue(boardTileA.UntransposedY == 3);

            BoardTile boardTileB = board.GetBoardTileAtCoordinates(4, 2);
            Assert.IsTrue(boardTileB.X == 4);
            Assert.IsTrue(boardTileB.UntransposedX == 2);
            Assert.IsTrue(boardTileB.Y == 2);
            Assert.IsTrue(boardTileB.UntransposedY == 4);
        }
    }
}
