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
        public void Test_PrintBoard()
        {
            Board board = new Board(rowCount: 7, columnCount: 10);

            board.SetCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.SetCharTile(X: 2, Y: 2, c: new CharTile('E'));
            board.SetCharTile(X: 3, Y: 2, c: new CharTile('L'));
            board.SetCharTile(X: 4, Y: 2, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 2, c: new CharTile('O'));

            string result = board.PrintBoard();
            Debug.WriteLine(result);

            Assert.IsTrue(board.RowCount == 7);
            Assert.IsTrue(board.ColumnCount == 10);
        }

        [TestMethod]
        public void GetBoardTileAtCoordinatesTests()
        {
            Board board = new(rowCount: 2, columnCount: 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1) != null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2) != null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(0, 1) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 0) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(0, 0) == null);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(-1, -1) == null);
        }

        [TestMethod]
        public void Test_CreateBoard_BoardTilesXsAndYs()
        {
            Board board = new(rowCount: 2, columnCount: 2);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1).X == 1);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 1).Y == 1);

            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2).X == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 2).Y == 2);
        }

        [TestMethod]
        public void BoardCreationException()
        {
            Board board;
            AssertThatCorrectExceptionIsThrown(() => board = new Board(0, 1), ExceptionMessages.BoardMustHaveAtLeastOneRow);
            AssertThatCorrectExceptionIsThrown(() => board = new Board(1, -1), ExceptionMessages.BoardMustHaveAtLeastOneColumn);
            AssertThatCorrectExceptionIsThrown(() => board = new Board(-3, 0), ExceptionMessages.BoardMustHaveAtLeastOneRow);
        }

        [TestMethod]
        public void SetCharTileTest()
        {
            Board board = new(1, 10);
            CharTile c = new('A');
            Assert.IsTrue(board.SetCharTile(1, 11, c) == null);
            Assert.IsTrue(board.SetCharTile(1, 10, c).Equals(c));
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 10).CharTile.Equals(c));
        }

        private static void AssertThatCorrectExceptionIsThrown(Action action, string message)
        {
            Exception rowError = Assert.ThrowsException<Exception>(() => action.Invoke());
            Assert.IsTrue(rowError.Message.Equals(message));
        }

        [TestMethod]
        public void CharTileCreationExceptions()
        {
            CharTile c;
            c = new CharTile('A');
            c = new CharTile('Z');

            AssertThatCorrectExceptionIsThrown(() => c = new CharTile('a'), ExceptionMessages.LetterCanOnlyBeBetweenAAndZ);
            AssertThatCorrectExceptionIsThrown(() => c = new CharTile('A', 0), ExceptionMessages.ScoreMustBeGreaterThan0);
        }

        [TestMethod]
        public void ReadHorizontalWordTest()
        {
            Board board = new Board(rowCount: 7, columnCount: 7);
            board.SetCharTile(X: 1, Y: 2, c: new CharTile('H'));
            board.SetCharTile(X: 1, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 1, Y: 4, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 5, c: new CharTile('L'));
            board.SetCharTile(X: 1, Y: 6, c: new CharTile('O'));

            board.SetCharTile(X: 2, Y: 2, c: new CharTile('F'));
            board.SetCharTile(X: 2, Y: 3, c: new CharTile('R'));
            board.SetCharTile(X: 2, Y: 4, c: new CharTile('I'));
            board.SetCharTile(X: 2, Y: 5, c: new CharTile('E'));
            board.SetCharTile(X: 2, Y: 6, c: new CharTile('N'));
            board.SetCharTile(X: 2, Y: 7, c: new CharTile('D'));

            board.SetCharTile(X: 3, Y: 1, c: new CharTile('B'));
            board.SetCharTile(X: 3, Y: 2, c: new CharTile('Y'));
            board.SetCharTile(X: 3, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 3, Y: 4, c: new CharTile('B'));
            board.SetCharTile(X: 3, Y: 5, c: new CharTile('Y'));
            board.SetCharTile(X: 3, Y: 6, c: new CharTile('E'));

            board.SetCharTile(X: 4, Y: 4, c: new CharTile('M'));
            board.SetCharTile(X: 4, Y: 5, c: new CharTile('A'));
            board.SetCharTile(X: 4, Y: 6, c: new CharTile('T'));
            board.SetCharTile(X: 4, Y: 7, c: new CharTile('E'));

            Debug.WriteLine(board.PrintBoard());

            HorizontalBoardWord word0 = board.GetHorizontalWordTilesAtCoordinates(-1, -1);
            Assert.IsTrue(word0 == null);

            HorizontalBoardWord horizontalBoardWord = board.GetHorizontalWordTilesAtCoordinates(1, 3);
            string word1 = horizontalBoardWord.GetWord();
            Assert.IsTrue(word1.Equals("HELLO"));

            string word2 = board.GetHorizontalWordTilesAtCoordinates(2, 5).GetWord();
            Assert.IsTrue(word2.Equals("FRIEND"));

            string word3 = board.GetHorizontalWordTilesAtCoordinates(3, 1).GetWord();
            Assert.IsTrue(word3.Equals("BYEBYE"));

            string word4 = board.GetHorizontalWordTilesAtCoordinates(4, 7).GetWord();
            Assert.IsTrue(word4.Equals("MATE"));
        }

        [TestMethod]
        public void ReadVerticalWordTest()
        {
            Board board = new Board(rowCount: 7, columnCount: 7);
            board.SetCharTile(X: 2, Y: 1, c: new CharTile('H'));
            board.SetCharTile(X: 3, Y: 1, c: new CharTile('E'));
            board.SetCharTile(X: 4, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 5, Y: 1, c: new CharTile('L'));
            board.SetCharTile(X: 6, Y: 1, c: new CharTile('O'));

            board.SetCharTile(X: 2, Y: 2, c: new CharTile('F'));
            board.SetCharTile(X: 3, Y: 2, c: new CharTile('R'));
            board.SetCharTile(X: 4, Y: 2, c: new CharTile('I'));
            board.SetCharTile(X: 5, Y: 2, c: new CharTile('E'));
            board.SetCharTile(X: 6, Y: 2, c: new CharTile('N'));
            board.SetCharTile(X: 7, Y: 2, c: new CharTile('D'));

            board.SetCharTile(X: 1, Y: 3, c: new CharTile('B'));
            board.SetCharTile(X: 2, Y: 3, c: new CharTile('Y'));
            board.SetCharTile(X: 3, Y: 3, c: new CharTile('E'));
            board.SetCharTile(X: 4, Y: 3, c: new CharTile('B'));
            board.SetCharTile(X: 5, Y: 3, c: new CharTile('Y'));
            board.SetCharTile(X: 6, Y: 3, c: new CharTile('E'));

            board.SetCharTile(X: 4, Y: 4, c: new CharTile('M'));
            board.SetCharTile(X: 5, Y: 4, c: new CharTile('A'));
            board.SetCharTile(X: 6, Y: 4, c: new CharTile('T'));
            board.SetCharTile(X: 7, Y: 4, c: new CharTile('E'));

            Debug.WriteLine(board.PrintBoard());

            VerticalBoardWord word0 = board.GetVerticalWordTilesAtCoordinates(-1, -1);
            Assert.IsTrue(word0 == null);

            string word1 = board.GetVerticalWordTilesAtCoordinates(3, 1).GetWord();
            Assert.IsTrue(word1.Equals("HELLO"));

            string word2 = board.GetVerticalWordTilesAtCoordinates(5, 2).GetWord();
            Assert.IsTrue(word2.Equals("FRIEND"));

            string word3 = board.GetVerticalWordTilesAtCoordinates(1, 3).GetWord();
            Assert.IsTrue(word3.Equals("BYEBYE"));

            string word4 = board.GetVerticalWordTilesAtCoordinates(7, 4).GetWord();
            Assert.IsTrue(word4.Equals("MATE"));
        }

        [TestMethod]
        public void TestHorizontalBoardWord()
        {
            HorizontalBoardWord horizontalWord;

            BoardTile char1 = new(1, 2, new CharTile('A'));
            BoardTile char2 = new(1, 3, new CharTile('B'));
            BoardTile char3 = new(1, 4, new CharTile('C'));
            List<BoardTile> charList = new() { char1, char2, char3 };
            horizontalWord = new HorizontalBoardWord(charList);
            Assert.IsTrue(horizontalWord.BoardTilesAreConnected());

            char1.X = 2;
            Assert.IsTrue(!horizontalWord.BoardTilesAreConnected());

            char1.X = 1;
            char1.Y = 1;
            Assert.IsTrue(!horizontalWord.BoardTilesAreConnected());

            char1.Y = 2;
            char3.Y = 5;
            Assert.IsTrue(!horizontalWord.BoardTilesAreConnected());
        }

        [TestMethod]
        public void TestVerticalBoardWord()
        {
            VerticalBoardWord VerticalWord;

            BoardTile char1 = new(2, 1, new CharTile('A'));
            BoardTile char2 = new(3, 1, new CharTile('B'));
            BoardTile char3 = new(4, 1, new CharTile('C'));
            List<BoardTile> charList = new() { char1, char2, char3 };

            VerticalWord = new VerticalBoardWord(charList);
            Assert.IsTrue(VerticalWord.BoardTilesAreConnected());

            char1.Y = 2;
            Assert.IsTrue(!VerticalWord.BoardTilesAreConnected());

            char1.Y = 1;
            char1.X = 1;
            Assert.IsTrue(!VerticalWord.BoardTilesAreConnected());

            char1.X = 2;
            char3.X = 5;
            Assert.IsTrue(!VerticalWord.BoardTilesAreConnected());
        }

        [TestMethod]
        public void TestGetAnchors()
        {
            Board board = new(rowCount: 5, columnCount: 5);
            Assert.IsTrue(board.GetAnchors().Count() == 0);

            board.SetCharTile(1, 1, 'H');
            Assert.IsTrue(board.GetAnchors().Count() == 2);
            board.SetCharTile(1, 1, null);

            board.SetCharTile(1, 2, 'H');
            Assert.IsTrue(board.GetAnchors().Count() == 3);
            board.SetCharTile(1, 2, null);

            board.SetCharTile(2, 2, 'H');
            Assert.IsTrue(board.GetAnchors().Count() == 4);

            board.SetCharTile(2, 3, 'E');
            Assert.IsTrue(board.GetAnchors().Count() == 6);

            board.SetCharTile(3, 3, 'E');
            Assert.IsTrue(board.GetAnchors().Count() == 7);

            board.SetCharTile(1, 3, 'O');
            Assert.IsTrue(board.GetAnchors().Count() == 7);

            board.SetCharTile(4, 3, 'O');
            Assert.IsTrue(board.GetAnchors().Count() == 9);

            board.SetCharTile(4, 2, 'Y');
            Assert.IsTrue(board.GetAnchors().Count() == 10);

            board.SetCharTile(4, 1, 'D');
            Assert.IsTrue(board.GetAnchors().Count() == 11);

            Debug.WriteLine(board.PrintBoard());
        }

        //https://boardgames.stackexchange.com/questions/38366/latest-collins-scrabble-words-list-in-text-file
        [TestMethod]
        [Ignore]
        public void GenerateDawgFromFile()
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
        public void TestDawgFile()
        {
            Dawg<bool> boingDawg = Globals.BoingDawg;
            Assert.IsTrue(boingDawg.Count() == 64);
        }

        [TestMethod]
        public void TestTransposition_1()
        {
            const int rowCount = 2;
            const int columnCount = 5;
            int[][] board = new int[rowCount][];
            board[0] = new int[columnCount];
            board[1] = new int[columnCount];

            board[0][0] = 0;
            board[0][1] = 1;
            board[0][2] = 2;
            board[0][3] = 3;
            board[0][4] = 4;
            board[1][0] = 5;
            board[1][1] = 6;
            board[1][2] = 7;
            board[1][3] = 8;
            board[1][4] = 9;

            int[][] newBoard = new int[columnCount][];
            for (int y = 0; y < columnCount; y++)
            {
                newBoard[y] = new int[rowCount];
                for (int x = 0; x < rowCount; x++)
                {
                    newBoard[y][x] = board[x][y];
                }
            }
        }

        [TestMethod]
        public void TestTransposition_2()
        {
            const int rowCount = 3;
            const int columnCount = 3;
            int[][] board = new int[rowCount][];
            board[0] = new int[columnCount];
            board[1] = new int[columnCount];
            board[2] = new int[columnCount];

            board[0][0] = 1;
            board[0][1] = 2;
            board[0][2] = 3;
            board[1][0] = 4;
            board[1][1] = 5;
            board[1][2] = 6;
            board[2][0] = 7;
            board[2][1] = 8;
            board[2][2] = 9;

            int[][] newBoard = new int[columnCount][];
            for (int y = 0; y < columnCount; y++)
            {
                newBoard[y] = new int[rowCount];
                for (int x = 0; x < rowCount; x++)
                {
                    newBoard[y][x] = board[x][y];
                }
            }
        }

        [TestMethod]
        public void Test_Transpose_Board()
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
            Debug.WriteLine(board.PrintBoard());

            board.Transpose();
            Assert.IsTrue(board.RowCount == 3);
            Assert.IsTrue(board.ColumnCount == 2);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'D');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(2, 1).Guid.Equals(guid));
            Debug.WriteLine(board.PrintBoard());

            board.Transpose();
            Assert.IsTrue(board.RowCount == 2);
            Assert.IsTrue(board.ColumnCount == 3);
            boardTile = board.GetBoardTileAtCoordinates(1, 2);
            Assert.IsTrue(boardTile.CharTile.Letter == 'B');
            Assert.IsTrue(boardTile.X == 1);
            Assert.IsTrue(boardTile.Y == 2);
            Assert.IsTrue(board.GetBoardTileAtCoordinates(1, 2).Guid.Equals(guid));
            Debug.WriteLine(board.PrintBoard());
        }

        [TestMethod]
        public void Test_GetAnchorsAndTheirCrossChecks_BoingDawg()
        {
            Board board = new(3, 7, Globals.BoingDawg);
            board.SetCharTile(2, 2, 'B');
            board.SetCharTile(2, 3, 'O');
            board.SetCharTile(2, 4, 'I');
            board.SetCharTile(2, 5, 'N');
            board.SetCharTile(2, 6, 'G');

            BoardTileCollection boardAnchors = board.GetAnchors();

            Debug.WriteLine(board.PrintBoard());

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

            Debug.WriteLine(board.PrintBoard());
        }

        [TestMethod]
        public void Test_GetAnchorsAndTheirCrossChecks_EnglishDawg()
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

        public DawgNode GetDawgNode(string word)
        {
            Dawg<bool> dawg = Globals.EnglishDawg;
            IEnumerable<KeyValuePair<string, bool>> wordsContainingPrefix = dawg.MatchPrefix(word);
            HashSet<char> lettersThatCanFollowPrefix = new();
            foreach (KeyValuePair<string, bool> wordContainingPrefix_Pair in wordsContainingPrefix)
            {
                string wordContainingPrefix = wordContainingPrefix_Pair.Key;
                if (!wordContainingPrefix.Equals(word)) lettersThatCanFollowPrefix.Add(wordContainingPrefix[word.Length]);
            }
            return new DawgNode(word, lettersThatCanFollowPrefix);
        }

        [TestMethod]
        public void Test_BuildWords()
        {
            Board board = new(1, 8);
            board.SetCharTile(1, 3, 'L');
            board.SetCharTile(1, 4, 'O');
            board.SetCharTile(1, 5, 'V');
            board.SetCharTile(1, 6, 'E');


            //string word = board.GetHorizontalWordTilesAtCoordinates(1, 3).GetWord();
            //foreach (KeyValuePair<BoardTile, HashSet<char>> anchorAndItsCrossChecks in anchorsWithCrossChecks)
            //{
            //    BoardTile anchor = anchorAndItsCrossChecks.Key;
            //    HashSet<char> crossChecks = anchorAndItsCrossChecks.Value;
            //    foreach (char ch in crossChecks)
            //    {
            //        if (!playerRack.Contains(ch)) continue;
            //    }
            //}

            //List<char> playerRack = Globals.GetEnglishCharactersArray().ToList();
            List<char> playerRack = "XDRSUNGLY".ToList();
            Dictionary<BoardTile, HashSet<char>> anchorsWithCrossChecks = board.GetCrossChecksForBoardTiles(board.GetAnchors());
            BoardTile boardTile = board.GetBoardTileAtCoordinates(1, 2);
            DawgNode node = GetDawgNode("");
            LeftPart("", node, 10, boardTile, anchorsWithCrossChecks[boardTile], playerRack, board);
        }

        public void LeftPart(string partialWord, DawgNode node, int limit, BoardTile anchor, HashSet<char> anchorCrossChecks, List<char> playerRack, Board board)
        {
            ExtendRight(partialWord, node, anchor, anchorCrossChecks, playerRack, board);
            if (limit <= 0) return;
            foreach (char edge in node.Edges)
            {
                if (!playerRack.Contains(edge)) continue;

                char rackChar = playerRack.FirstOrDefault(t => t == edge);
                playerRack.Remove(rackChar);
                string partialWordPlusEdge = partialWord + rackChar.ToString();
                DawgNode nextNode = GetDawgNode(partialWordPlusEdge);
                LeftPart(partialWordPlusEdge, nextNode, limit - 1, anchor, anchorCrossChecks, playerRack, board);
            }
        }

        private void ExtendRight(string partialWord, DawgNode node, BoardTile boardTile, HashSet<char> boardTileCrossChecks, List<char> playerRack, Board board)
        {

            if (boardTile == null)
            {
                if (Globals.EnglishDawg[partialWord]) LegalMove(partialWord);
            }

            else if (boardTile.CharTile == null)
            {
                if (Globals.EnglishDawg[partialWord]) LegalMove(partialWord);
                foreach (char edge in node.Edges)
                {
                    if (!playerRack.Contains(edge) || (boardTileCrossChecks != null && !boardTileCrossChecks.Contains(edge))) continue;

                    char rackChar = playerRack.FirstOrDefault(t => t == edge);
                    playerRack.Remove(rackChar);
                    string partialWordPlusEdge = partialWord + rackChar.ToString();
                    DawgNode nextNode = GetDawgNode(partialWordPlusEdge);
                    BoardTile nextBoardTile = board.GetBoardTileAtCoordinates(boardTile.X, boardTile.Y + 1);
                    HashSet<char> nextBoardTileCrossChecks = board.GetCrossChecksForBoardTile(nextBoardTile);
                    ExtendRight(partialWordPlusEdge, nextNode, nextBoardTile, nextBoardTileCrossChecks, playerRack, board);
                    playerRack.Add(rackChar);
                }
            }
            else
            {
                char charOnBoardTile = boardTile.CharTile.Letter;
                if (!node.Edges.Contains(charOnBoardTile)) return;

                string partialWordPlusEdge = partialWord + charOnBoardTile.ToString();
                DawgNode nextNode = GetDawgNode(partialWordPlusEdge);
                BoardTile nextBoardTile = board.GetBoardTileAtCoordinates(boardTile.X, boardTile.Y + 1);
                HashSet<char> nextBoardTileCrossChecks = board.GetCrossChecksForBoardTile(nextBoardTile);
                ExtendRight(partialWordPlusEdge, nextNode, nextBoardTile, nextBoardTileCrossChecks, playerRack, board);
            }
        }

        private void LegalMove(string partialWord)
        {
            Debug.WriteLine(partialWord);
        }
    }
}
