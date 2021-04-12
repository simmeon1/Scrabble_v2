using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class XYCoordinates__Comparer_Tests
    {
        [TestMethod]
        public void TestCorrectnessOfIEqualityComparerImplementation()
        {
            XYCoordinates c1 = new(2, 3);
            XYCoordinates c2 = new(2, 3);
            XYCoordinates c3 = new(3, 2);

            HashSet<XYCoordinates> hs = new (comparer: new XYCoordinates_Comparer());
            hs.Add(c1);
            hs.Add(c2);
            Assert.IsTrue(hs.Count == 1);
            
            hs.Add(c3);
            Assert.IsTrue(hs.Count == 2);
        }
    }
}
