using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTests
{
    [DebuggerDisplay("{Word}, Count = {Edges.Count}")]
    public class DawgNode
    {
        public string Word { get; set; }
        public HashSet<char> Edges { get; set; }

        public DawgNode(string word, HashSet<char> edges)
        {
            Word = word;
            Edges = edges;
        }
    }
}