using System.Collections.Generic;

namespace ClassLibrary
{
    public abstract class BoardTileCollection
    {
        protected List<BoardTile> BoardTiles { get; set; }

        public BoardTileCollection(List<BoardTile> boardTiles = null)
        {
            BoardTiles = boardTiles ?? new List<BoardTile>();
        }
    }
}