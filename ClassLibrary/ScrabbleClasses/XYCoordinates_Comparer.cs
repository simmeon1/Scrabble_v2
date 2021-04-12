using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ClassLibrary
{
    public class XYCoordinates_Comparer : IEqualityComparer<XYCoordinates>
    {
        public bool Equals(XYCoordinates a, XYCoordinates b)
        {
            return a != null && a.HasSameCoordinatesAs(b);
        }

        /// <summary>https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode</summary>
        public int GetHashCode([DisallowNull] XYCoordinates obj)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + obj.X.GetHashCode();
                hash = hash * 23 + obj.Y.GetHashCode();
                return hash;
            }
        }
    }
}
