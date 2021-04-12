using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class BoardCrossCheckCollector_Tests
    {
        [TestMethod]
        public void GetCrossChecksForBoardTiles_CreateABoardWithWordBoingAndUsingBoingDawg_AssertThatCrossChecksAreCorrectBeforeAndAfterTransposition()
        {
            Board board = new(3, 7);
            board.PlaceCharTile(2, 2, 'B');
            board.PlaceCharTile(2, 3, 'O');
            board.PlaceCharTile(2, 4, 'I');
            board.PlaceCharTile(2, 5, 'N');
            board.PlaceCharTile(2, 6, 'G');

            BoardAnchorCollector boardAnchorCollector = new BoardAnchorCollector();
            BoardTileCollection boardAnchors = boardAnchorCollector.GetAnchors(board);

            BoardCrossCheckCollector boardCrossCheckCollector = new BoardCrossCheckCollector(board, Globals.BoingDawg);
            Dictionary<BoardTile, HashSet<char>> crossChecksForNormalBoard = boardCrossCheckCollector.GetCrossChecksForBoardTiles(boardAnchors);

            Assert.IsTrue(crossChecksForNormalBoard.Count == 12);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(1, 2)].Count == 2);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(1, 3)].Count == 17);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(1, 4)].Count == 13);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(1, 5)].Count == 5);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(1, 6)].Count == 2);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(3, 2)].Count == 5);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(3, 3)].Count == 16);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(3, 4)].Count == 6);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(3, 5)].Count == 5);
            Assert.IsTrue(crossChecksForNormalBoard[board.GetBoardTileAtCoordinates(3, 6)].Count == 3);

            BoardTransposer transposer = new(board);
            transposer.TransposeBoard();

            Dictionary<BoardTile, HashSet<char>> crossChecksForTransposedBoard = boardCrossCheckCollector.GetCrossChecksForBoardTiles(boardAnchors);

            Assert.IsTrue(crossChecksForTransposedBoard[board.GetBoardTileAtCoordinates(1, 2)].Count == 0);
            Assert.IsTrue(crossChecksForTransposedBoard[board.GetBoardTileAtCoordinates(7, 2)].Count == 1);

            transposer.TransposeBoard();
            Assert.IsTrue(board.RowCount == 3);
            Assert.IsTrue(board.ColumnCount == 7);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 3).CharTile.Letter == 'O');
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 3).X == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 3).Y == 3);
        }

        [TestMethod]
        public void GetCrossChecksForBoardTiles_CreateBoardWithEnglishDawg_AssertThatDawgIsWorkingAndThatCrossChecksAreCollected()
        {
            Board board = new(3, 7);
            board.PlaceCharTile(2, 2, 'B');
            board.PlaceCharTile(2, 3, 'O');
            board.PlaceCharTile(2, 4, 'I');
            board.PlaceCharTile(2, 5, 'N');
            board.PlaceCharTile(2, 6, 'G');

            BoardAnchorCollector boardAnchorCollector = new();
            BoardCrossCheckCollector boardCrossCheckCollector = new BoardCrossCheckCollector(board, Globals.EnglishDawg);
            Dictionary<BoardTile, HashSet<char>> anchorsWithCrossChecks = boardCrossCheckCollector.GetCrossChecksForBoardTiles(boardAnchorCollector.GetAnchors(board));
            Assert.IsTrue(anchorsWithCrossChecks.Count > 0);
        }
    }
}
