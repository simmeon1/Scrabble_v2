using System.Diagnostics;

namespace ClassLibrary
{
    [DebuggerDisplay("{PrintBoard()}")]
    public class BoardTransposer
    {
        public static void Transpose(Board board)
        {
            BoardTile[][] newBoard = new BoardTile[board.ColumnCount][];
            for (int y = 0; y < board.ColumnCount; y++)
            {
                newBoard[y] = new BoardTile[board.RowCount];
                for (int x = 0; x < board.RowCount; x++)
                {
                    BoardTile boardTile = board.Tiles[x][y];
                    newBoard[y][x] = boardTile;
                    int temp = boardTile.X;
                    boardTile.X = boardTile.Y;
                    boardTile.Y = temp;
                }
            }
            board.Tiles = newBoard;
            int temp2 = board.RowCount;
            board.RowCount = board.ColumnCount;
            board.ColumnCount = temp2;
        }
    }
}