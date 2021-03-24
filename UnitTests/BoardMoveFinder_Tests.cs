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
            List<char> englishAlphabet = Globals.GetEnglishCharactersArray().ToList();
            List<char> playerRack = englishAlphabet;
            playerRack.AddRange(englishAlphabet);
            playerRack.AddRange(englishAlphabet);
            playerRack.AddRange(englishAlphabet);

            BoardMoveFinder boardMoveFinder = new(board, Globals.EnglishDawg);
            List<string> words = boardMoveFinder.GetPossibleMoves(anchor, playerRack);
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
            List<char> englishAlphabet = Globals.GetEnglishCharactersArray().ToList();
            List<char> playerRack = englishAlphabet;
            playerRack.AddRange(englishAlphabet);
            playerRack.AddRange(englishAlphabet);

            BoardMoveFinder boardMoveFinder = new(board, Globals.EnglishDawg);
            List<string> words = boardMoveFinder.GetPossibleMoves(anchor, playerRack);
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
            List<char> englishAlphabet = Globals.GetEnglishCharactersArray().ToList();
            List<char> playerRack = englishAlphabet;
            playerRack.AddRange(englishAlphabet);

            BoardMoveFinder boardMoveFinder = new(board, Globals.EnglishDawg);
            List<string> words = boardMoveFinder.GetPossibleMoves(anchor, playerRack);
            Assert.IsTrue(words.Count == 13);
        }
    }
}
