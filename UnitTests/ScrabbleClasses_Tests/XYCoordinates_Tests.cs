using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class XYCoordinates_Tests
    {
        [TestMethod]
        public void HasSameCoordinatesAs_TestCorrectness()
        {
            XYCoordinates c1 = new(2, 3);
            XYCoordinates c2 = new(2, 3);
            XYCoordinates c3 = null;
            XYCoordinates c4 = new(3, 2);

            Assert.IsTrue(c1.HasSameCoordinatesAs(c2));
            Assert.IsFalse(c1.HasSameCoordinatesAs(c3));
            Assert.IsFalse(c1.HasSameCoordinatesAs(c4));
        }
    }
}
