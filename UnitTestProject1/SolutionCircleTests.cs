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
        public void RepoCircle()
        {
            var circlesService = new CirclesService();
            var solutionA = new SolutionNode("SolutionA");
            var solutionB = new SolutionNode("SolutionB");
            var solutionC = new SolutionNode("SolutionC");
            var artifactA = solutionA.CreatesArtifact("ArtifactA");
            var artifactB = solutionB.CreatesArtifact("ArtifactB");
            var artifactC = solutionC.CreatesArtifact("ArtifactC");

            // the circle
            artifactA.DependsOn(artifactB);
            artifactB.DependsOn(artifactC);
            artifactC.DependsOn(artifactA);

            NodeRepository repo = new NodeRepository();
            repo.Add(solutionA);
            repo.Add(solutionB);
            repo.Add(solutionC);
            IList<ICircle> circles = circlesService.FindCircles(repo);
            Assert.IsNotNull(circles, "Even when there is no circle, there should be a valid list");
            Assert.AreEqual(3, circles.Count, "list should have three circles");
            Assert.AreEqual("SolutionA:ArtifactA,SolutionB:ArtifactB,SolutionC:ArtifactC,SolutionA:ArtifactA", circles[0].ToString());
            Assert.AreEqual("SolutionB:ArtifactB,SolutionC:ArtifactC,SolutionA:ArtifactA,SolutionB:ArtifactB", circles[1].ToString());
            Assert.AreEqual("SolutionC:ArtifactC,SolutionA:ArtifactA,SolutionB:ArtifactB,SolutionC:ArtifactC", circles[2].ToString());
        }

        [TestMethod]
        public void RepoSolutionCircle()
        {
            var circlesService = new CirclesService();
            var solutionA = new SolutionNode("SolutionA");
            var solutionB = new SolutionNode("SolutionB");
            var solutionC = new SolutionNode("SolutionC");
            var artifactA = solutionA.CreatesArtifact("ArtifactA");
            var artifactD = solutionA.CreatesArtifact("ArtifactD");

            var artifactB = solutionB.CreatesArtifact("ArtifactB");
            var artifactC = solutionC.CreatesArtifact("ArtifactC");

            // the circle
            artifactA.DependsOn(artifactB);
            artifactB.DependsOn(artifactC);
            artifactC.DependsOn(artifactD);

            NodeRepository repo = new NodeRepository();
            repo.Add(solutionA);
            repo.Add(solutionB);
            repo.Add(solutionC);
            IList<ICircle> circles = circlesService.FindCircles(repo);
            Assert.IsNotNull(circles, "Even when there is no circle, there should be a valid list");
            Assert.AreEqual(1, circles.Count, "list should have one circles");
            Assert.AreEqual("SolutionA:ArtifactA,SolutionB:ArtifactB,SolutionC:ArtifactC,SolutionA:ArtifactD", circles[0].ToString());

        }
    }
}
