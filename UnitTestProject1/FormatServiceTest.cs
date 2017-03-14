using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stevpet.Tools.Build;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for FormatServiceTest
    /// </summary>
    [TestClass]
    public class FormattingServiceTest
    {

        [TestMethod]
        public void FormatServiceTest()
        {
            string name = "Joa.JewelEarth.Seismic.sln";
            var service = new NodeFormattingService();
            string formatted = service.FormatSolution(new SolutionNode(name));
            Assert.AreEqual("J.JE.Seismic",formatted);

        }

        [TestMethod]
        public void FormatArtifactTest()
        {
            string name = "Joa.JewelEarth.Seismic";
            var service = new NodeFormattingService();
            string formatted = service.FormatArtifact(new ArtifactNode(null,name,null));
            Assert.AreEqual("J.JE.Seismic", formatted);

        }
    }
}
