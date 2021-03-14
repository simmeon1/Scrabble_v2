using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class AnchorTileCollection : IList<AnchorTile>
    {
        protected List<AnchorTile> AnchorTiles { get; set; }
        public AnchorTileCollection(List<AnchorTile> AnchorTiles = null)
        {
            this.AnchorTiles = AnchorTiles ?? new List<AnchorTile>();
        }

        public int Count => ((ICollection<AnchorTile>)AnchorTiles).Count;

        public bool IsReadOnly => ((ICollection<AnchorTile>)AnchorTiles).IsReadOnly;

        public AnchorTile this[int index] { get => ((IList<AnchorTile>)AnchorTiles)[index]; set => ((IList<AnchorTile>)AnchorTiles)[index] = value; }


        public int IndexOf(AnchorTile item)
        {
            return ((IList<AnchorTile>)AnchorTiles).IndexOf(item);
        }

        public void Insert(int index, AnchorTile item)
        {
            ((IList<AnchorTile>)AnchorTiles).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<AnchorTile>)AnchorTiles).RemoveAt(index);
        }

        public void Add(AnchorTile item)
        {
            ((ICollection<AnchorTile>)AnchorTiles).Add(item);
        }

        public void Clear()
        {
            ((ICollection<AnchorTile>)AnchorTiles).Clear();
        }

        public bool Contains(AnchorTile item)
        {
            return ((ICollection<AnchorTile>)AnchorTiles).Contains(item);
        }

        public void CopyTo(AnchorTile[] array, int arrayIndex)
        {
            ((ICollection<AnchorTile>)AnchorTiles).CopyTo(array, arrayIndex);
        }

        public bool Remove(AnchorTile item)
        {
            return ((ICollection<AnchorTile>)AnchorTiles).Remove(item);
        }

        public IEnumerator<AnchorTile> GetEnumerator()
        {
            return ((IEnumerable<AnchorTile>)AnchorTiles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)AnchorTiles).GetEnumerator();
        }
    }
}