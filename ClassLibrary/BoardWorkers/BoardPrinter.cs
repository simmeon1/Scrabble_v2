using System.Diagnostics;
using System.Text;

namespace ClassLibrary
{
    public class BoardPrinter
    {
        public const string PrintBoard_Delimiter = "-------------------------------";
        public static string PrintBoard(Board board)
        {
            BoardAnchorCollector boardAnchorCollector = new();
            BoardTileCollection anchors = boardAnchorCollector.GetAnchors(board);
            StringBuilder sb = new(PrintBoard_Delimiter);
            foreach (BoardTile[] rows in board.Tiles)
            {
                if (sb.Length != 0) sb.Append('\n');
                foreach (BoardTile tile in rows) sb.Append(anchors.Contains(tile) ? "[=]" : $"[{tile.PrintChar()}]");
            }
            sb.Append('\n');
            sb.Append(PrintBoard_Delimiter);
            return sb.ToString();
        }
    }
}