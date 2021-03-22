using DawgSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ClassLibrary
{
    public class MoveFinder
    {
        private Board Board { get; set; }
        private List<string> ValidWords { get; set; }
        private List<char> PlayerRack { get; set; }
        private BoardTile StartingBoardTile { get; set; }
        private bool LeftPartIsAlreadyProvided { get; set; }

        public MoveFinder(Board board)
        {
            Board = board;
        }

        public List<string> GetPossibleMoves(BoardTile anchor, List<char> playerRack)
        {
            ValidWords = new List<string>();
            PlayerRack = playerRack;
            StartingBoardTile = anchor;

            HorizontalBoardWord wordToTheLeftOfAnchor = Board.GetHorizontalWordTilesAtCoordinates(StartingBoardTile.X, StartingBoardTile.Y - 1);
            string partialWord = wordToTheLeftOfAnchor?.GetWord() ?? "";
            LeftPartIsAlreadyProvided = partialWord.Length > 0;

            StartingBoardTile.CrossChecks = Board.GetCrossChecksForBoardTile(StartingBoardTile);

            BoardTileCollection boardAnchors = Board.GetAnchors();

            DawgNode node = GetDawgNode(partialWord);
            int limit = Board.GetNumberOfNonAnchorTilesToTheLeftOfABoardTile(StartingBoardTile, boardAnchors);
            LeftPart(node, limit, anchor);
            return ValidWords;
        }

        private DawgNode GetDawgNode(string word)
        {
            if (word.IsNullOrEmpty()) return new DawgNode(word, Globals.GetEnglishCharactersArray().ToHashSet());

            Dawg<bool> dawg = Board.Dawg;
            IEnumerable<KeyValuePair<string, bool>> wordsContainingPrefix = dawg.MatchPrefix(word);
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
                    HashSet<char> boardTileCrossChecks = boardTile.CrossChecks;
                    if (!PlayerRack.Contains(edge) || (boardTileCrossChecks != null && !boardTileCrossChecks.Contains(edge))) continue;

                    char rackChar = PlayerRack.FirstOrDefault(t => t == edge);
                    PlayerRack.Remove(rackChar);
                    string partialWordPlusEdge = partialWord + rackChar.ToString();
                    DawgNode nextNode = GetDawgNode(partialWordPlusEdge);
                    BoardTile nextBoardTile = Board.GetBoardTileAtCoordinates(boardTile.X, boardTile.Y + 1);
                    if (nextBoardTile != null) nextBoardTile.CrossChecks = Board.GetCrossChecksForBoardTile(nextBoardTile);
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
                HashSet<char> nextBoardTileCrossChecks = Board.GetCrossChecksForBoardTile(nextBoardTile);
                if (nextBoardTile != null) nextBoardTile.CrossChecks = Board.GetCrossChecksForBoardTile(nextBoardTile);
                ExtendRight(nextNode, nextBoardTile);
            }
        }

        private void AddWordToValidWordsIfValid(string partialWord)
        {
            if (!Globals.EnglishDawg[partialWord]) return;
            ValidWords.Add(partialWord);
            Debug.WriteLine(partialWord);
        }
    }
}