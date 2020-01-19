using System;
using GCCollectX;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GCExtensionTest
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void TestGC1()
        {
            GCX.CollectX();
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void TestGC2()
        {
            GCX.CollectX(true,true);
            Assert.IsTrue(true);
        }
    }
}
