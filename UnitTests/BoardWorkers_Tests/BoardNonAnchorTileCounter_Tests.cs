using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BoardNonAnchorTileCounter_Tests
    {
        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfLIsZero()
        {
            Board board = new(1, 8);
            board.PlaceCharTile(1, 3, 'L');
            board.PlaceCharTile(1, 4, 'O');
            board.PlaceCharTile(1, 5, 'V');
            board.PlaceCharTile(1, 6, 'E');

            BoardAnchorCollector boardAnchorCollector = new();
            BoardNonAnchorTileCounter boardNonAnchorTileCounter = new BoardNonAnchorTileCounter(board);
            int count = boardNonAnchorTileCounter.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 3), boardAnchorCollector.GetAnchors(board));
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfOIsOne()
        {
            Board board = new(1, 8);
            board.PlaceCharTile(1, 3, 'L');
            board.PlaceCharTile(1, 4, 'O');
            board.PlaceCharTile(1, 5, 'V');
            board.PlaceCharTile(1, 6, 'E');

            BoardAnchorCollector boardAnchorCollector = new();
            BoardNonAnchorTileCounter boardNonAnchorTileCounter = new BoardNonAnchorTileCounter(board);
            int count1 = boardNonAnchorTileCounter.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 2), boardAnchorCollector.GetAnchors(board));
            Assert.IsTrue(count1 == 1);
        }

        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfTheLeftmostAnchorIsOne()
        {
            Board board = new(1, 8);
            board.PlaceCharTile(1, 3, 'L');
            board.PlaceCharTile(1, 4, 'O');
            board.PlaceCharTile(1, 5, 'V');
            board.PlaceCharTile(1, 6, 'E');

            BoardAnchorCollector boardAnchorCollector = new BoardAnchorCollector();
            BoardNonAnchorTileCounter boardNonAnchorTileCounter = new BoardNonAnchorTileCounter(board);
            int count1 = boardNonAnchorTileCounter.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 2), boardAnchorCollector.GetAnchors(board));
            Assert.IsTrue(count1 == 1);
        }

        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfEIs3()
        {
            Board board = new(1, 8);
            board.PlaceCharTile(1, 3, 'L');
            board.PlaceCharTile(1, 4, 'O');
            board.PlaceCharTile(1, 5, 'V');
            board.PlaceCharTile(1, 6, 'E');

            BoardAnchorCollector boardAnchorCollector = new BoardAnchorCollector();
            BoardNonAnchorTileCounter boardNonAnchorTileCounter = new BoardNonAnchorTileCounter(board);
            int count2 = boardNonAnchorTileCounter.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 6), boardAnchorCollector.GetAnchors(board));
            Assert.IsTrue(count2 == 3);
        }
    }
}
