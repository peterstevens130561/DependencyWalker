using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyWalker
{
    [TestClass]
    public class SolutionNodeTests
    {
        [TestMethod]
        public void StartBuildOneNode()
        {
            var node = new SolutionNode("mypath");
            Assert.AreEqual("mypath", node.Location);
        }

        [TestMethod]
        public void NodeWithOneDependency()
        {
            var node = new SolutionNode("mypath");
            var reference = new SolutionNode("reference");
            node.Dependencies.Add(reference);
            Assert.AreEqual(1, node.Dependencies.Count);
            Assert.AreEqual(reference, node.Dependencies[0]);
        }

        [TestMethod]
        public void NodeWithOneDependent()
        {
            var node = new SolutionNode("mypath");
            var dependent = new SolutionNode("dependent");
            node.Dependents.Add(dependent);
            Assert.AreEqual(1, node.Dependents.Count);
            Assert.AreEqual(dependent, node.Dependents[0]);
        }
    }
}
