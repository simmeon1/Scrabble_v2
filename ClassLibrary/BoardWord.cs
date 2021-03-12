using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class BoardWord : IList<BoardTile>
    {
        private List<BoardTile> BoardTiles { get; set; }
        public BoardWord(List<BoardTile> boardTiles)
        {
            BoardTiles = boardTiles ?? new List<BoardTile>();
        }

        public BoardTile this[int index] { get => ((IList<BoardTile>)BoardTiles)[index]; set => ((IList<BoardTile>)BoardTiles)[index] = value; }

        public int Count => ((ICollection<BoardTile>)BoardTiles).Count;

        public bool IsReadOnly => ((ICollection<BoardTile>)BoardTiles).IsReadOnly;

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

        public IEnumerator<BoardTile> GetEnumerator()
        {
            return ((IEnumerable<BoardTile>)BoardTiles).GetEnumerator();
        }

        public int IndexOf(BoardTile item)
        {
            return ((IList<BoardTile>)BoardTiles).IndexOf(item);
        }

        public void Insert(int index, BoardTile item)
        {
            ((IList<BoardTile>)BoardTiles).Insert(index, item);
        }

        public bool Remove(BoardTile item)
        {
            return ((ICollection<BoardTile>)BoardTiles).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<BoardTile>)BoardTiles).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)BoardTiles).GetEnumerator();
        }

        public string GetWord()
        {
            StringBuilder sb = new StringBuilder();
            foreach (BoardTile boardTile in BoardTiles) sb.Append(boardTile.PrintChar());
            return sb.ToString();
        }
    }
}
