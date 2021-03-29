using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BoardPrinter_Tests
    {
        [TestMethod]
        public void PrintBoard_InitiateBoardWith5RowsAnd5Columns_ResultStringContains143CharactersIncludingBoardDelimiters()
        {
            Board board = new(rowCount: 5, columnCount: 5);

            board.PlaceCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.PlaceCharTile(X: 2, Y: 2, c: new CharTile('E'));
            board.PlaceCharTile(X: 3, Y: 2, c: new CharTile('L'));
            board.PlaceCharTile(X: 4, Y: 2, c: new CharTile('L'));
            board.PlaceCharTile(X: 5, Y: 2, c: new CharTile('O'));

            string boardString = BoardPrinter.PrintBoard(board);
            Assert.IsTrue(boardString.Length == 143);
        }
    }
}
