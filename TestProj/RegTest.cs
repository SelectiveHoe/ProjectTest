using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandProject;
using System.ServiceModel;

namespace TestProj
{
    [TestClass]
    public class RegTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Server.ServerService obj = new Server.ServerService();
            obj.Registration()
        }
    }
}
