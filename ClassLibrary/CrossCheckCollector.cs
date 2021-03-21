using DawgSharp;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    public class CrossCheckCollector
    {
        public Dawg<bool> Dawg { get; set; }
        public Board Board { get; set; }
        public BoardTileCollection BoardTileCollection { get; set; }
        public Dictionary<BoardTile, HashSet<char>> GetCrossChecksForBoardTiles(Board board, BoardTileCollection boardTileCollection)
        {
            Board = board;
            BoardTileCollection = boardTileCollection;
            Dawg = board.Dawg;
            return GetCrossChecksForBoardTiles();
        }

        private Dictionary<BoardTile, HashSet<char>> GetCrossChecksForBoardTiles()
        {
            char[] charsFromAlphabet = Globals.GetEnglishCharactersArray();
            HashSet<char> charsFromAlphabetHashSet = charsFromAlphabet.ToHashSet();

            Dictionary<BoardTile, HashSet<char>> tilesAndTheirCrossChecks = new();
            foreach (BoardTile boardTile in BoardTileCollection)
            {
                if (boardTile == null)
                {
                    tilesAndTheirCrossChecks.Add(boardTile, charsFromAlphabetHashSet);
                    continue;
                }

                if (boardTile.CharTile != null) continue;

                tilesAndTheirCrossChecks.Add(boardTile, new HashSet<char>());
                foreach (char ch in charsFromAlphabet)
                {
                    Board.SetCharTile(boardTile.X, boardTile.Y, ch);
                    VerticalBoardWord verticalWord = Board.GetVerticalWordTilesAtCoordinates(boardTile.X, boardTile.Y);
                    if (verticalWord.Count < 2)
                    {
                        tilesAndTheirCrossChecks[boardTile] = charsFromAlphabetHashSet;
                        Board.SetCharTile(boardTile.X, boardTile.Y, null);
                        break;
                    }
                    if (Dawg[verticalWord.GetWord()] == true) tilesAndTheirCrossChecks[boardTile].Add(ch);
                    Board.SetCharTile(boardTile.X, boardTile.Y, null);
                }
            }
            return tilesAndTheirCrossChecks;
        }
    }
}
