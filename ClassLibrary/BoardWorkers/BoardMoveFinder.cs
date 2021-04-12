using DawgSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ClassLibrary
{
    public class BoardMoveFinder
    {
        private Board Board { get; set; }
        private Dawg<bool> Dawg { get; set; }
        private List<BoardWord> ValidWords { get; set; }
        private List<char> PlayerRack { get; set; }
        private BoardTile StartingBoardTile { get; set; }
        private bool LeftPartIsAlreadyProvided { get; set;  }
        private Dictionary<BoardTile, HashSet<char>> BoardTilesAndTheirCrossChecks { get; set; }
        private BoardCrossCheckCollector BoardCrossCheckCollector { get; set; }
        private BoardWordRetriever BoardWordRetriever { get; set; }

        public BoardMoveFinder(Board board, Dawg<bool> dawg)
        {
            Board = board;
            Dawg = dawg;
            BoardCrossCheckCollector = new BoardCrossCheckCollector(board, dawg);
            BoardWordRetriever = new BoardWordRetriever(Board);
        }

        public List<BoardWord> GetPossibleMoves(BoardTile anchor, List<char> playerRack)
        {
            ValidWords = new List<BoardWord>();
            PlayerRack = playerRack;
            StartingBoardTile = anchor;
            BoardTilesAndTheirCrossChecks = new Dictionary<BoardTile, HashSet<char>>();

            HorizontalBoardWord wordToTheLeftOfAnchor = BoardWordRetriever.GetHorizontalWordTilesAtCoordinates(StartingBoardTile.X, StartingBoardTile.Y - 1);
            string partialWord = wordToTheLeftOfAnchor?.GetWord() ?? "";
            LeftPartIsAlreadyProvided = partialWord.Length > 0;

            BoardTilesAndTheirCrossChecks.Add(StartingBoardTile, BoardCrossCheckCollector.GetCrossChecksForBoardTile(StartingBoardTile));

            BoardAnchorCollector boardAnchorCollector = new();
            BoardTileCollection boardAnchors = boardAnchorCollector.GetAnchors(Board);

            DawgNode node = GetDawgNode(partialWord);
            BoardNonAnchorTileCounter boardNonAnchorTileCounter = new(Board);
            int limit = boardNonAnchorTileCounter.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(StartingBoardTile, boardAnchors);
            LeftPart(node, limit, anchor);
            return ValidWords;
        }

        private DawgNode GetDawgNode(string word)
        {
            if (word.IsNullOrEmpty()) return new DawgNode(word, Globals.GetEnglishCharactersArray().ToHashSet());

            IEnumerable<KeyValuePair<string, bool>> wordsContainingPrefix = Dawg.MatchPrefix(word);
            HashSet<char> lettersThatCanFollowPrefix = new();
            foreach (KeyValuePair<string, bool> wordContainingPrefix_Pair in wordsContainingPrefix)
            {
                string wordContainingPrefix = wordContainingPrefix_Pair.Key;
                if (!wordContainingPrefix.Equals(word)) lettersThatCanFollowPrefix.Add(wordContainingPrefix[word.Length]);
            }
            return new DawgNode(word, lettersThatCanFollowPrefix);
        }

        private void LeftPart(DawgNode node, int limit, BoardTile anchor)
        {
            ExtendRight(node, anchor);
            if (LeftPartIsAlreadyProvided || limit <= 0) return;
            foreach (char edge in node.Edges)
            {
                if (!PlayerRack.Contains(edge)) continue;

                char rackChar = PlayerRack.FirstOrDefault(t => t == edge);
                PlayerRack.Remove(rackChar);
                string partialWord = node.Word;
                string partialWordPlusEdge = partialWord + rackChar.ToString();
                DawgNode nextNode = GetDawgNode(partialWordPlusEdge);
                LeftPart(nextNode, limit - 1, anchor);
                PlayerRack.Add(rackChar);
            }
        }

        private void ExtendRight(DawgNode node, BoardTile boardTile)
        {
            string partialWord = node.Word;
            if (boardTile == null) AddWordToValidWordsIfValid(partialWord);
            else if (boardTile.CharTile == null)
            {
                if (boardTile != StartingBoardTile) AddWordToValidWordsIfValid(partialWord);
                foreach (char edge in node.Edges)
                {
                    HashSet<char> boardTileCrossChecks = GetBoardTileCrossChecks(boardTile);
                    if (!PlayerRack.Contains(edge) || (boardTileCrossChecks != null && !boardTileCrossChecks.Contains(edge))) continue;

                    char rackChar = PlayerRack.FirstOrDefault(t => t == edge);
                    PlayerRack.Remove(rackChar);
                    string partialWordPlusEdge = partialWord + rackChar.ToString();
                    DawgNode nextNode = GetDawgNode(partialWordPlusEdge);
                    BoardTile nextBoardTile = Board.GetBoardTileAtCoordinates(boardTile.X, boardTile.Y + 1);
                    SetBoardTileCrossChecks(nextBoardTile);
                    ExtendRight(nextNode, nextBoardTile);
                    PlayerRack.Add(rackChar);
                }
            }
            else
            {
                char charOnBoardTile = boardTile.CharTile.Letter;
                if (!node.Edges.Contains(charOnBoardTile)) return;

                string partialWordPlusEdge = partialWord + charOnBoardTile.ToString();
                DawgNode nextNode = GetDawgNode(partialWordPlusEdge);
                BoardTile nextBoardTile = Board.GetBoardTileAtCoordinates(boardTile.X, boardTile.Y + 1);
                SetBoardTileCrossChecks(nextBoardTile);
                ExtendRight(nextNode, nextBoardTile);
            }
        }

        private void SetBoardTileCrossChecks(BoardTile boardTile)
        {
            if (boardTile == null) return;

            HashSet<char> crossChecks = BoardCrossCheckCollector.GetCrossChecksForBoardTile(boardTile);
            if (BoardTilesAndTheirCrossChecks.ContainsKey(boardTile))
            {
                BoardTilesAndTheirCrossChecks[boardTile] = crossChecks;
            }
            else
            {
                BoardTilesAndTheirCrossChecks.Add(boardTile, crossChecks);
            }
        }

        private HashSet<char> GetBoardTileCrossChecks(BoardTile boardTile)
        {
            return BoardTilesAndTheirCrossChecks.ContainsKey(boardTile) ? BoardTilesAndTheirCrossChecks[boardTile] : new HashSet<char>();
        }

        private void AddWordToValidWordsIfValid(string partialWord)
        {
            if (!Globals.EnglishDawg[partialWord]) return;

            HorizontalBoardWord wordTiles = BoardWordRetriever.GetHorizontalWordTilesAtCoordinates(StartingBoardTile.X, StartingBoardTile.Y);
            ValidWords.Add(wordTiles);
            //string word = wordTiles.GetWord();
            //Debug.WriteLine(partialWord);
        }
    }
}