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
    public class UnitTest1
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
            CharTile placedCharTile = board.SetCharTile(x, y, charTileToPlace);
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
            CharTile placedCharTile = board.SetCharTile(99, 99, charTileToPlace);
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
            board.SetCharTile(X: x, Y: y, c: new CharTile('W'));
            Assert.IsTrue(board.GetBoardTileAtCoordinates(x, y).CharTile.Letter == 'W');
        }

        [TestMethod]
        public void PrintBoard_InitiateBoardWith5RowsAnd5Columns_ResultStringContains143CharactersIncludingBoardDelimiters()
        {
            Board board = new(rowCount: 5, columnCount: 5);

            board.SetCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.SetCharTile(X: 2, Y: 2, c: new CharTile('E'));
            board.SetCharTile(X: 3, Y: 2, c: new CharTile('L'));
            board.SetCharTile(X: 4, Y: 2, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 2, c: new CharTile('O'));

            string boardString = board.PrintBoard();
            Assert.IsTrue(boardString.Length == 143);
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
            InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(() => board = new Board(0, 1), ExceptionMessages.BoardMustHaveAtLeastOneRow);
        }

        [TestMethod]
        public void NewBoard_CreationWithBadColumnCount_IncorrectColumnNumberExceptionIsThrown()
        {
            Board board;
            InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(() => board = new Board(1, -1), ExceptionMessages.BoardMustHaveAtLeastOneColumn);
        }

        private static void InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(Action action, string message)
        {
            Exception rowError = Assert.ThrowsException<Exception>(() => action.Invoke());
            Assert.IsTrue(rowError.Message.Equals(message));
        }

        [TestMethod]
        public void NewCharTile_InitialisationWithLowerCaseChar_ThrowExceptionThatCharMustBeUppercase()
        {
            CharTile c;
            InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(() => c = new CharTile('a'), ExceptionMessages.LetterCanOnlyBeBetweenAAndZ);
        }
        
        [TestMethod]
        public void NewCharTile_InitialisationWith0Score_ThrowExceptionThatScoreMustBeMoreThan0()
        {
            CharTile c;
            InvokeActionAndAssertThatCorrectExceptionMessageIsThrown(() => c = new CharTile('A', 0), ExceptionMessages.ScoreMustBeGreaterThan0);
        }

        [TestMethod]
        public void GetHorizontalWordTilesAtCoordinates_WordHelloSetAtMiddleOfRow_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 1, columnCount: 7);
            board.SetCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.SetCharTile(X: 1, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 5, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 6, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            HorizontalBoardWord horizontalBoardWord;

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 2);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
            
            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 3);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
            
            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 4);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
            
            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 5);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
            
            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 6);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
        }


        [TestMethod]
        public void GetHorizontalWordTilesAtCoordinates_WordHelloSetAtStartOfRow_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 1, columnCount: 6);
            board.SetCharTile(X: 1, Y: 1, c: new CharTile('H'));
            board.SetCharTile(X: 1, Y: 2, c: new CharTile('E'));
            board.SetCharTile(X: 1, Y: 3, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 5, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            HorizontalBoardWord horizontalBoardWord;

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 1);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 2);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 3);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 4);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 5);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetHorizontalWordTilesAtCoordinates_WordHelloSetAtEndOfRow_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 1, columnCount: 6);
            board.SetCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.SetCharTile(X: 1, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 5, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 6, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            HorizontalBoardWord horizontalBoardWord;

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 2);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 3);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 4);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 5);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));

            horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 6);
            Assert.IsTrue(horizontalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetVerticalWordTilesAtCoordinates_WordHelloSetAtMiddleOfColumn_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 7, columnCount: 1);
            board.SetCharTile(X: 2, Y: 1, c: new CharTile('H'));
            board.SetCharTile(X: 3, Y: 1, c: new CharTile('E'));
            board.SetCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 6, Y: 1, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            VerticalBoardWord VerticalBoardWord;

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(2, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(3, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(4, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(5, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(6, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetVerticalWordTilesAtCoordinates_WordHelloSetAtStartOfColumn_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 6, columnCount: 1);
            board.SetCharTile(X: 1, Y: 1, c: new CharTile('H'));
            board.SetCharTile(X: 2, Y: 1, c: new CharTile('E'));
            board.SetCharTile(X: 3, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 1, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            VerticalBoardWord VerticalBoardWord;

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(1, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(2, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(3, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(4, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(5, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetVerticalWordTilesAtCoordinates_WordHelloSetAtEndOfColumn_AssertThatFullWordIsRetrievedByLookingAtAnyOfTheInvolvedBoardTiles()
        {
            Board board = new(rowCount: 6, columnCount: 1);
            board.SetCharTile(X: 2, Y: 1, c: new CharTile('H'));
            board.SetCharTile(X: 3, Y: 1, c: new CharTile('E'));
            board.SetCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 6, Y: 1, c: new CharTile('O'));

            const string wordToLookFor = "HELLO";
            VerticalBoardWord VerticalBoardWord;

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(2, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(3, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(4, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(5, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));

            VerticalBoardWord = board.GetVerticalWordTilesAtCoordinates(6, 1);
            Assert.IsTrue(VerticalBoardWord.GetWord().Equals(wordToLookFor));
        }

        [TestMethod]
        public void GetAnchors_SetCharactersOnBoardTiles_AssertThatAnchorCountIsCorrectAfterEachPlacement()
        {
            Board board = new(rowCount: 5, columnCount: 5);
            Assert.IsTrue(board.GetAnchors().Count == 0);

            board.SetCharTile(1, 1, 'H');
            Assert.IsTrue(board.GetAnchors().Count == 2);
            board.SetCharTile(1, 1, null);

            board.SetCharTile(1, 2, 'H');
            Assert.IsTrue(board.GetAnchors().Count == 3);
            board.SetCharTile(1, 2, null);

            board.SetCharTile(2, 2, 'H');
            Assert.IsTrue(board.GetAnchors().Count == 4);

            board.SetCharTile(2, 3, 'E');
            Assert.IsTrue(board.GetAnchors().Count == 6);

            board.SetCharTile(3, 3, 'E');
            Assert.IsTrue(board.GetAnchors().Count == 7);

            board.SetCharTile(1, 3, 'O');
            Assert.IsTrue(board.GetAnchors().Count == 7);

            board.SetCharTile(4, 3, 'O');
            Assert.IsTrue(board.GetAnchors().Count == 9);

            board.SetCharTile(4, 2, 'Y');
            Assert.IsTrue(board.GetAnchors().Count == 10);

            board.SetCharTile(4, 1, 'D');
            Assert.IsTrue(board.GetAnchors().Count == 11);
        }

        //https://boardgames.stackexchange.com/questions/38366/latest-collins-scrabble-words-list-in-text-file
        [TestMethod]
        [Ignore]
        public void BoingDawg_Generate()
        {
            const string textFile = "boing_crosschecks.txt";
            //const string textFile = "englishWords.txt";

            const string binFile = "boingDAWG.bin";
            //const string binFile = "englishDawg.bin";

            string fileContents = File.ReadAllText(textFile);
            List<string> boingWords = Regex.Matches(fileContents, "\\w+").Select(m => m.Value).ToList();

            DawgBuilder<bool> dawgBuilder = new();
            foreach (string word in boingWords) dawgBuilder.Insert(word, true);

            Dawg<bool> dawg = dawgBuilder.BuildDawg(); // Computer is working.  Please wait ...

            using (FileStream file = File.Create(binFile)) dawg.SaveTo(file);

            //Now read the file back in and check if a particular word is in the dictionary:
            Dawg<bool> dawg2 = Dawg<bool>.Load(File.Open(binFile, FileMode.Open));
        }

        [TestMethod]
        public void BoingDawg_ReadDawg_CountMustBe64()
        {
            Dawg<bool> boingDawg = Globals.BoingDawg;
            Assert.IsTrue(boingDawg.Count() == 64);
        }

        [TestMethod]
        public void Transpose_TransposingA2By3BoardTo3By2_AssertThatTheBoardTilesXsAndYsAndCharsAreAsExpected()
        {
            Board board = new(rowCount: 2, columnCount: 3);
            board.SetCharTile(1, 1, 'A');
            board.SetCharTile(1, 2, 'B');
            board.SetCharTile(1, 3, 'C');
            board.SetCharTile(2, 1, 'D');
            board.SetCharTile(2, 2, 'E');
            board.SetCharTile(2, 3, 'F');

            Assert.IsTrue(board.RowCount == 2);
            Assert.IsTrue(board.ColumnCount == 3);
            BoardTile boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Guid guid = boardTile.Guid;
            Assert.IsTrue(boardTile.CharTile.Letter == 'B');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);

            board.Transpose();
            Assert.IsTrue(board.RowCount == 3);
            Assert.IsTrue(board.ColumnCount == 2);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'D');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 1).Guid.Equals(guid));

            board.Transpose();
            Assert.IsTrue(board.RowCount == 2);
            Assert.IsTrue(board.ColumnCount == 3);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'B');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 2).Guid.Equals(guid));
        }

        [TestMethod]
        public void GetCrossChecksForBoardTiles_CreateABoardWithWordBoingAndUsingBoingDawg_AssertThatCrossChecksAreCorrectBeforeAndAfterTransposition()
        {
            Board board = new(3, 7, Globals.BoingDawg);
            board.SetCharTile(2, 2, 'B');
            board.SetCharTile(2, 3, 'O');
            board.SetCharTile(2, 4, 'I');
            board.SetCharTile(2, 5, 'N');
            board.SetCharTile(2, 6, 'G');

            BoardTileCollection boardAnchors = board.GetAnchors();
            Dictionary<BoardTile, HashSet<char>> crossChecksForNormalBoard = board.GetCrossChecksForBoardTiles(boardAnchors);

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

            board.Transpose();
            Dictionary<BoardTile, HashSet<char>> crossChecksForTransposedBoard = board.GetCrossChecksForBoardTiles(boardAnchors);

            Assert.IsTrue(crossChecksForTransposedBoard[board.GetBoardTileAtCoordinates(1, 2)].Count == 0);
            Assert.IsTrue(crossChecksForTransposedBoard[board.GetBoardTileAtCoordinates(7, 2)].Count == 1);

            board.Transpose();

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
            board.SetCharTile(2, 2, 'B');
            board.SetCharTile(2, 3, 'O');
            board.SetCharTile(2, 4, 'I');
            board.SetCharTile(2, 5, 'N');
            board.SetCharTile(2, 6, 'G');
            Dictionary<BoardTile, HashSet<char>> anchorsWithCrossChecks = board.GetCrossChecksForBoardTiles(board.GetAnchors());
            Assert.IsTrue(anchorsWithCrossChecks.Count > 0);
        }

        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfLIsZero()
        {
            Board board = new(1, 8);
            board.SetCharTile(1, 3, 'L');
            board.SetCharTile(1, 4, 'O');
            board.SetCharTile(1, 5, 'V');
            board.SetCharTile(1, 6, 'E');

            int count = board.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 3), board.GetAnchors());
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfOIsOne()
        {
            Board board = new(1, 8);
            board.SetCharTile(1, 3, 'L');
            board.SetCharTile(1, 4, 'O');
            board.SetCharTile(1, 5, 'V');
            board.SetCharTile(1, 6, 'E');

            int count1 = board.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 2), board.GetAnchors());
            Assert.IsTrue(count1 == 1);
        }

        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfTheLeftmostAnchorIsOne()
        {
            Board board = new(1, 8);
            board.SetCharTile(1, 3, 'L');
            board.SetCharTile(1, 4, 'O');
            board.SetCharTile(1, 5, 'V');
            board.SetCharTile(1, 6, 'E');

            int count1 = board.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 2), board.GetAnchors());
            Assert.IsTrue(count1 == 1);
        }

        [TestMethod]
        public void GetNumberOfNonAnchorTilesToTheLeftOfABoardTile_PlaceWordLoveInTheMiddleOfTheRow_CountOfSaidTilesToTheLeftOfEIs3()
        {
            Board board = new(1, 8);
            board.SetCharTile(1, 3, 'L');
            board.SetCharTile(1, 4, 'O');
            board.SetCharTile(1, 5, 'V');
            board.SetCharTile(1, 6, 'E');
            
            int count2 = board.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(board.GetBoardTileAtCoordinates(1, 6), board.GetAnchors());
            Assert.IsTrue(count2 == 3);
        }

        [TestMethod]
        public void GetPossibleMoves_FindAllWordsStartingWithLove_BuildingWordsFromRightmostAnchor_CountMustBe60()
        {
            Board board = new(1, 15);
            board.SetCharTile(1, 2, 'L');
            board.SetCharTile(1, 3, 'O');
            board.SetCharTile(1, 4, 'V');
            board.SetCharTile(1, 5, 'E');

            BoardTile anchor = board.GetBoardTileAtCoordinates(1, 6);
            List<char> englishAlphabet = Globals.GetEnglishCharactersArray().ToList();
            List<char> playerRack = englishAlphabet;
            playerRack.AddRange(englishAlphabet);
            playerRack.AddRange(englishAlphabet);
            playerRack.AddRange(englishAlphabet);
            List<string> words = board.GetPossibleMoves(anchor, playerRack);
            Assert.IsTrue(words.Count == 60);
        }

        [TestMethod]
        public void GetPossibleMoves_FindAllWordsContainingLove_BuildingWordsFromLeftmostAnchor_CountMustBe89()
        {
            Board board = new(1, 19);
            board.SetCharTile(1, 6, 'L');
            board.SetCharTile(1, 7, 'O');
            board.SetCharTile(1, 8, 'V');
            board.SetCharTile(1, 9, 'E');

            BoardTile anchor = board.GetBoardTileAtCoordinates(1, 5);
            List<char> englishAlphabet = Globals.GetEnglishCharactersArray().ToList();
            List<char> playerRack = englishAlphabet;
            playerRack.AddRange(englishAlphabet);
            playerRack.AddRange(englishAlphabet);
            List<string> words = board.GetPossibleMoves(anchor, playerRack);
            Assert.IsTrue(words.Count == 89);
        }

        [TestMethod]
        public void GetPossibleMoves_FindAllWordsEndingWithLove_BuildingWordsFromLeftmostAnchor_CountMustBe13()
        {
            Board board = new(1, 10);
            board.SetCharTile(1, 7, 'L');
            board.SetCharTile(1, 8, 'O');
            board.SetCharTile(1, 9, 'V');
            board.SetCharTile(1, 10, 'E');

            BoardTile anchor = board.GetBoardTileAtCoordinates(1, 6);
            List<char> englishAlphabet = Globals.GetEnglishCharactersArray().ToList();
            List<char> playerRack = englishAlphabet;
            playerRack.AddRange(englishAlphabet);
            List<string> words = board.GetPossibleMoves(anchor, playerRack);
            Assert.IsTrue(words.Count == 13);
        }
    }
}
