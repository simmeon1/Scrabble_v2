using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class BoardTileCollection : IList<BoardTile>
    {
        protected List<BoardTile> BoardTiles { get; set; }
        public BoardTileCollection(List<BoardTile> boardTiles = null)
        {
            BoardTiles = boardTiles ?? new List<BoardTile>();
        }

        public int Count => ((ICollection<BoardTile>)BoardTiles).Count;

        public bool IsReadOnly => ((ICollection<BoardTile>)BoardTiles).IsReadOnly;

        public BoardTile this[int index] { get => ((IList<BoardTile>)BoardTiles)[index]; set => ((IList<BoardTile>)BoardTiles)[index] = value; }


        public int IndexOf(BoardTile item)
        {
            return ((IList<BoardTile>)BoardTiles).IndexOf(item);
        }

        public void Insert(int index, BoardTile item)
        {
            ((IList<BoardTile>)BoardTiles).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<BoardTile>)BoardTiles).RemoveAt(index);
        }

        public void Add(BoardTile item)
        {
            ((ICollection<BoardTile>)BoardTiles).Add(item);
        }

        public void Clear()
        {
            ((ICollection<BoardTile>)BoardTiles).Clear();
        }

        public bool Contains(BoardTile item)
        {
            return ((ICollection<BoardTile>)BoardTiles).Contains(item);
        }

        public void CopyTo(BoardTile[] array, int arrayIndex)
        {
            ((ICollection<BoardTile>)BoardTiles).CopyTo(array, arrayIndex);
        }

        public bool Remove(BoardTile item)
        {
            return ((ICollection<BoardTile>)BoardTiles).Remove(item);
        }

        public IEnumerator<BoardTile> GetEnumerator()
        {
            return ((IEnumerable<BoardTile>)BoardTiles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)BoardTiles).GetEnumerator();
        }
    }
}