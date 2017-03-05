using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stevpet.Tools.Build;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class SolutionCircleTests
    {
        /// <summary>
        /// Simple circle through three solutions
        /// </summary>
        [TestMethod]
        public void SimpleCircle()
        {
            var circlesService = new CirclesService();
            var solutionA = new SolutionNode("SolutionA");
            var solutionB = new SolutionNode("SolutionB");
            var solutionC = new SolutionNode("SolutionC");
            var artifactA = solutionA.CreatesArtifact("ArtifactA");
            var artifactB = solutionB.CreatesArtifact("ArtifactB");
            var artifactC = solutionC.CreatesArtifact("ArtifactC");

            // the circle
            artifactA.Add(artifactB);
            artifactB.Add(artifactC);
            artifactC.Add(artifactA);

            IList<ICircle> circles = circlesService.FindCircles(artifactA);
            Assert.IsNotNull(circles, "Even when there is no circle, there should be a valid list");
            Assert.AreEqual(1, circles.Count, "list should have one circles");
            Assert.AreEqual("SolutionA:ArtifactA,SolutionB:ArtifactB,SolutionC:ArtifactC,SolutionA:ArtifactA", circles[0].ToString());
        }
    }
}
