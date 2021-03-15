using DawgSharp;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class AnchorCrossCheckCollector
    {
        public Dawg<bool> Dawg { get; set; }
        public Board Board { get; set; }
        public BoardTileCollection AnchorCollection { get; set; }
        public Dictionary<BoardTile, HashSet<char>> GetAnchorsAndTheirCrossChecks(Board board, BoardTileCollection anchorCollection, Dawg<bool> dawg)
        {
            Board = board;
            AnchorCollection = anchorCollection;
            Dawg = dawg;
            return GetAnchorsAndTheirCrossChecks_DoTheWork();
        }

        public Dictionary<BoardTile, HashSet<char>> GetAnchorsAndTheirCrossChecks(Board board, Dawg<bool> dawg)
        {
            Board = board;
            AnchorCollection = board.GetAnchors();
            Dawg = dawg;
            return GetAnchorsAndTheirCrossChecks_DoTheWork();
        }

        private Dictionary<BoardTile, HashSet<char>> GetAnchorsAndTheirCrossChecks_DoTheWork()
        {
            char[] charsFromAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            Dictionary<BoardTile, HashSet<char>> tilesAndTheirCrossChecks_FirstPass = DoVerticalCrossCheckForAnchors(charsFromAlphabet);
            Board.Transpose();
            Dictionary<BoardTile, HashSet<char>> tilesAndTheirCrossChecks_SecondPass = DoVerticalCrossCheckForAnchors(charsFromAlphabet);
            Board.Transpose();

            Dictionary<BoardTile, HashSet<char>> tilesAndTheirCrossChecks_Final = new();
            foreach (BoardTile anchor in AnchorCollection)
            {
                tilesAndTheirCrossChecks_Final.Add(anchor, new HashSet<char>());
                foreach (char ch in charsFromAlphabet)
                {
                    if (tilesAndTheirCrossChecks_FirstPass[anchor].Contains(ch) && tilesAndTheirCrossChecks_SecondPass[anchor].Contains(ch)) tilesAndTheirCrossChecks_Final[anchor].Add(ch);
                }
            }
            return tilesAndTheirCrossChecks_Final;
        }

        private Dictionary<BoardTile, HashSet<char>> DoVerticalCrossCheckForAnchors(char[] charsFromAlphabet)
        {
            Dictionary<BoardTile, HashSet<char>> tilesAndTheirCrossChecks = new();
            foreach (BoardTile anchor in AnchorCollection)
            {
                tilesAndTheirCrossChecks.Add(anchor, new HashSet<char>());
                foreach (char ch in charsFromAlphabet)
                {
                    Board.SetCharTile(anchor.X, anchor.Y, ch);
                    VerticalBoardWord verticalWord = Board.GetVerticalWordTilesAtCoordinates(anchor.X, anchor.Y);
                    if (verticalWord.Count < 2 || Dawg[verticalWord.GetWord()] == true) tilesAndTheirCrossChecks[anchor].Add(ch);
                    Board.SetCharTile(anchor.X, anchor.Y, null);
                }
            }
            return tilesAndTheirCrossChecks;
        }
    }
}
