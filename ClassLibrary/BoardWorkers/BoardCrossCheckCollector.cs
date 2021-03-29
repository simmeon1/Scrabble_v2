using DawgSharp;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    public class BoardCrossCheckCollector
    {
        public Dawg<bool> Dawg { get; set; }
        public Board Board { get; set; }
        public BoardCrossCheckCollector(Board board, Dawg<bool> dawg)
        {
            Dawg = dawg;
            Board = board;
        }

        public HashSet<char> GetCrossChecksForBoardTile(BoardTile boardTile)
        {
            if (boardTile == null) return null;
            BoardTileCollection boardTileCollection = new(new List<BoardTile>() { boardTile });
            Dictionary<BoardTile, HashSet<char>> crossChecksForBoardTileCollection = GetCrossChecksForBoardTiles(boardTileCollection);
            return crossChecksForBoardTileCollection.ContainsKey(boardTile) ? crossChecksForBoardTileCollection[boardTile] : null;
        }

        public Dictionary<BoardTile, HashSet<char>> GetCrossChecksForBoardTiles(BoardTileCollection boardTileCollection)
        {
            BoardWordRetriever boardWordRetriever = new(Board);
            char[] charsFromAlphabet = Globals.GetEnglishCharactersArray();
            HashSet<char> charsFromAlphabetHashSet = charsFromAlphabet.ToHashSet();

            Dictionary<BoardTile, HashSet<char>> tilesAndTheirCrossChecks = new();
            foreach (BoardTile boardTile in boardTileCollection)
            {

                if (boardTile.CharTile != null) continue;

                tilesAndTheirCrossChecks.Add(boardTile, new HashSet<char>());
                if (!boardWordRetriever.BoardTileIsVerticallyConnectedToCharTiles(boardTile.X, boardTile.Y))
                {
                    tilesAndTheirCrossChecks[boardTile] = charsFromAlphabetHashSet;
                    continue;
                }

                foreach (char ch in charsFromAlphabet)
                {
                    Board.PlaceCharTile(boardTile.X, boardTile.Y, ch);
                    VerticalBoardWord verticalWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(boardTile.X, boardTile.Y);
                    if (Dawg[verticalWord.GetWord()] == true) tilesAndTheirCrossChecks[boardTile].Add(ch);
                    Board.RemoveCharTile(boardTile.X, boardTile.Y);
                }
            }
            return tilesAndTheirCrossChecks;
        }
    }
}
