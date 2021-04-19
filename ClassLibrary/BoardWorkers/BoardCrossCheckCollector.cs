using ClassLibrary.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    public class BoardCrossCheckCollector
    {
        public IDawgWithAlphabet DawgWithAlphabet { get; set; }
        public Board Board { get; set; }
        public BoardCrossCheckCollector(Board board, IDawgWithAlphabet dawg)
        {
            DawgWithAlphabet = dawg;
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
            char[] charsFromAlphabet = DawgWithAlphabet.GetAlphabet();
            HashSet<char> charsFromAlphabetHashSet = charsFromAlphabet.ToHashSet();

            Dictionary<BoardTile, HashSet<char>> tilesAndTheirCrossChecks = new();
            foreach (BoardTile boardTile in boardTileCollection)
            {

                if (boardTile.CharTile != null) continue;

                tilesAndTheirCrossChecks.Add(boardTile, new HashSet<char>());

                BoardTileVerticalWordConnectionChecker verticallityChecker = new(Board);
                if (!verticallityChecker.BoardTileIsVerticallyPartOfAWord(boardTile.X, boardTile.Y))
                {
                    tilesAndTheirCrossChecks[boardTile] = charsFromAlphabetHashSet;
                    continue;
                }

                foreach (char ch in charsFromAlphabet)
                {
                    Board.PlaceCharTile(boardTile.X, boardTile.Y, ch);
                    VerticalBoardWord verticalWord = boardWordRetriever.GetVerticalWordTilesAtCoordinates(boardTile.X, boardTile.Y);
                    if (DawgWithAlphabet.IsWordValid(verticalWord.GetWord())) tilesAndTheirCrossChecks[boardTile].Add(ch);
                    Board.RemoveCharTile(boardTile.X, boardTile.Y);
                }
            }
            return tilesAndTheirCrossChecks;
        }
    }
}
