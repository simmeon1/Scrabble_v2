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
    public class BoardWordRetriever_Tests
    {
        [TestMethod]
        public void GetHorizontalWordTilesAtCoordinates_WordHelloSetAtMiddleOfRow_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 1, columnCount: 7);
            board.PlaceCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.PlaceCharTile(X: 1, Y: 3, c: new CharTile('E'));
            board.PlaceCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.PlaceCharTile(X: 1, Y: 5, c: new CharTile('L'));
            board.PlaceCharTile(X: 1, Y: 6, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            HorizontalBoardWord horizontalBoardWord;

            BoardWordRetriever boardWordRetriever = new BoardWordRetriever(board);

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 2);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 3);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 4);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 5);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 6);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
        }


        [TestMethod]
        public void GetHorizontalWordTilesAtCoordinates_WordHelloSetAtStartOfRow_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 1, columnCount: 6);
            board.PlaceCharTile(X: 1, Y: 1, c: new CharTile('H'));
            board.PlaceCharTile(X: 1, Y: 2, c: new CharTile('E'));
            board.PlaceCharTile(X: 1, Y: 3, c: new CharTile('L'));
            board.PlaceCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.PlaceCharTile(X: 1, Y: 5, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            HorizontalBoardWord horizontalBoardWord;

            BoardWordRetriever boardWordRetriever = new BoardWordRetriever(board);

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 1);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 2);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 3);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 4);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 5);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetHorizontalWordTilesAtCoordinates_WordHelloSetAtEndOfRow_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 1, columnCount: 6);
            board.PlaceCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.PlaceCharTile(X: 1, Y: 3, c: new CharTile('E'));
            board.PlaceCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.PlaceCharTile(X: 1, Y: 5, c: new CharTile('L'));
            board.PlaceCharTile(X: 1, Y: 6, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            HorizontalBoardWord horizontalBoardWord;

            BoardWordRetriever boardWordRetriever = new BoardWordRetriever(board);

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 2);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 3);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 4);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 5);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = boardWordRetriever.GetHorizontalWordTilesAtCoordinates(1, 6);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetVerticalWordTilesAtCoordinates_WordHelloSetAtMiddleOfColumn_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {

            Board board = new(rowCount: 7, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, c: new CharTile('H'));
            board.PlaceCharTile(X: 3, Y: 1, c: new CharTile('E'));
            board.PlaceCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.PlaceCharTile(X: 5, Y: 1, c: new CharTile('L'));
            board.PlaceCharTile(X: 6, Y: 1, c: new CharTile('O'));

            BoardWordRetriever boardWordRetriever = new BoardWordRetriever(board);

            const string wordToLookFor = "HELLO";
            VerticalBoardWord VerticalBoardWord;

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(2, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(3, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(4, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(5, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(6, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetVerticalWordTilesAtCoordinates_WordHelloSetAtStartOfColumn_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 6, columnCount: 1);
            board.PlaceCharTile(X: 1, Y: 1, c: new CharTile('H'));
            board.PlaceCharTile(X: 2, Y: 1, c: new CharTile('E'));
            board.PlaceCharTile(X: 3, Y: 1, c: new CharTile('L'));
            board.PlaceCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.PlaceCharTile(X: 5, Y: 1, c: new CharTile('O'));

            BoardWordRetriever boardWordRetriever = new BoardWordRetriever(board);

            const string wordToLookFor = "HELLO";
            VerticalBoardWord VerticalBoardWord;

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(1, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(2, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(3, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(4, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(5, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetVerticalWordTilesAtCoordinates_WordHelloSetAtEndOfColumn_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 6, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, c: new CharTile('H'));
            board.PlaceCharTile(X: 3, Y: 1, c: new CharTile('E'));
            board.PlaceCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.PlaceCharTile(X: 5, Y: 1, c: new CharTile('L'));
            board.PlaceCharTile(X: 6, Y: 1, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            VerticalBoardWord VerticalBoardWord;

            BoardWordRetriever boardWordRetriever = new BoardWordRetriever(board);

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(2, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(3, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(4, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(5, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(6, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void BoardTileIsVerticallyConnectedToCharTiles_TestOnNonEmptyBoardTile_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, c: 'H');

            BoardWordRetriever boardWordRetriever = new(board);
            Assert.IsTrue(boardWordRetriever.BoardTileIsVerticallyConnectedToCharTiles(x, y) == true);
        }

        [TestMethod]
        public void BoardTileIsVerticallyConnectedToCharTiles_TestOnEmptyBoardTile_NoNeighboringVerticalCharTiles_ResultIsFalse()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);

            BoardWordRetriever boardWordRetriever = new(board);
            Assert.IsTrue(boardWordRetriever.BoardTileIsVerticallyConnectedToCharTiles(x, y) == false);
        }

        [TestMethod]
        public void BoardTileIsVerticallyConnectedToCharTiles_TestOnEmptyBoardTile_HasUpperCharTileNeighbor_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);
            board.PlaceCharTile(X: 1, Y: 1, 'H');

            BoardWordRetriever boardWordRetriever = new(board);
            Assert.IsTrue(boardWordRetriever.BoardTileIsVerticallyConnectedToCharTiles(x, y) == true);
        }

        [TestMethod]
        public void BoardTileIsVerticallyConnectedToCharTiles_TestOnEmptyBoardTile_HasLowerCharTileNeighbor_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);
            board.PlaceCharTile(X: 3, Y: 1, 'H');

            BoardWordRetriever boardWordRetriever = new(board);
            Assert.IsTrue(boardWordRetriever.BoardTileIsVerticallyConnectedToCharTiles(x, y) == true);
        }
        
        [TestMethod]
        public void BoardTileIsVerticallyConnectedToCharTiles_TestOnEmptyBoardTile_HasUpperAndLowerCharTileNeighbor_ResultIsTrue()
        {
            int x = 2;
            int y = 1;
            Board board = new(rowCount: 3, columnCount: 1);
            board.PlaceCharTile(X: 2, Y: 1, null);
            board.PlaceCharTile(X: 3, Y: 1, 'H');
            board.PlaceCharTile(X: 1, Y: 1, 'H');

            BoardWordRetriever boardWordRetriever = new(board);
            Assert.IsTrue(boardWordRetriever.BoardTileIsVerticallyConnectedToCharTiles(x, y) == true);
        }
        
        [TestMethod]
        public void BoardTileIsVerticallyConnectedToCharTiles_TestOnEmptyBoardTile_OnlyOneRowAndColumn_ResultIsFalse()
        {
            int x = 1;
            int y = 1;
            Board board = new(rowCount: 1, columnCount: 1);

            BoardWordRetriever boardWordRetriever = new(board);
            Assert.IsTrue(boardWordRetriever.BoardTileIsVerticallyConnectedToCharTiles(x, y) == false);
        }
    }
}
