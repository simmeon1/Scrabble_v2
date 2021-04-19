using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class BoardMoveFinder_Tests
    {
        [TestMethod]
        public void GetPossibleMoves_FindAllWordsStartingWithLove_BuildingWordsFromRightmostAnchor_CountMustBe60()
        {
            Board board = new(1, 15);
            board.PlaceCharTile(1, 2, 'L');
            board.PlaceCharTile(1, 3, 'O');
            board.PlaceCharTile(1, 4, 'V');
            board.PlaceCharTile(1, 5, 'E');

            BoardTile anchor = board.GetBoardTileAtCoordinates(1, 6);

            PlayerRackWithUnlimitedCharTiles playerRackWithUnlimitedCharTiles = new();
            BoardMoveFinder boardMoveFinder = new(board, Globals.EnglishDawg);
            List<BoardWord> words = boardMoveFinder.GetPossibleMoves(anchor, playerRackWithUnlimitedCharTiles);
            Assert.IsTrue(words.Count == 60);
        }

        [TestMethod]
        public void GetPossibleMoves_FindAllWordsContainingLove_BuildingWordsFromLeftmostAnchor_CountMustBe89()
        {
            Board board = new(1, 19);
            board.PlaceCharTile(1, 6, 'L');
            board.PlaceCharTile(1, 7, 'O');
            board.PlaceCharTile(1, 8, 'V');
            board.PlaceCharTile(1, 9, 'E');

            BoardTile anchor = board.GetBoardTileAtCoordinates(1, 5);

            PlayerRackWithUnlimitedCharTiles playerRackWithUnlimitedCharTiles = new();
            BoardMoveFinder boardMoveFinder = new(board, Globals.EnglishDawg);
            List<BoardWord> words = boardMoveFinder.GetPossibleMoves(anchor, playerRackWithUnlimitedCharTiles);
            Assert.IsTrue(words.Count == 89);
        }

        [TestMethod]
        public void GetPossibleMoves_FindAllWordsEndingWithLove_BuildingWordsFromLeftmostAnchor_CountMustBe13()
        {
            Board board = new(1, 10);
            board.PlaceCharTile(1, 7, 'L');
            board.PlaceCharTile(1, 8, 'O');
            board.PlaceCharTile(1, 9, 'V');
            board.PlaceCharTile(1, 10, 'E');

            BoardTile anchor = board.GetBoardTileAtCoordinates(1, 6);

            PlayerRackWithUnlimitedCharTiles playerRackWithUnlimitedCharTiles = new();

            BoardMoveFinder boardMoveFinder = new(board, Globals.EnglishDawg);
            List<BoardWord> words = boardMoveFinder.GetPossibleMoves(anchor, playerRackWithUnlimitedCharTiles);
            Assert.IsTrue(words.Count == 13);
        }

        private class PlayerRackWithUnlimitedCharTiles : IPlayerRack
        {
            public void AddCharTile(char rackChar)
            {
                return;
            }

            public bool ContainsCharTile(char edge)
            {
                return true;
            }

            public char TakeCharTile(char edge)
            {
                return edge;
            }
        }
    }
}
