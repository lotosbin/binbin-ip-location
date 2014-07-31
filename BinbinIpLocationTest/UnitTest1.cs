using System;
using BinbinIpLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinbinIpLocationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var location = IpLocation.GetLocation("221.178.189.237", IpLocationProviders.pconline);
            Assert.AreEqual("221.178.189.237", location.IP);
            Assert.AreEqual("江苏省", location.Province);
            Assert.AreEqual("连云港市", location.City);
            Assert.AreEqual("", location.Region);
        }
    }
}
