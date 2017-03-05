using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stevpet.Tools.Build;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class CircleFinderTests
    {
        [TestMethod]
        public void OneNode()
        {
            var circlesService = new CirclesService();
            var nodeRepository = new NodeRepository();
            var node = new Node("a");
            nodeRepository.Add(node);

            IList<ICircle> circles = circlesService.FindCircles(node);
            Assert.IsNotNull(circles, "Even when there is no circle, there should be a valid list");
            Assert.AreEqual(0, circles.Count, "list should be empty");
        }

        [TestMethod]
        public void TwosNode()
        {
            var circlesService = new CirclesService();
            var nodeRepository = new NodeRepository();
            var nodeA = new Node("a");
            var nodeB = new Node("b");
            nodeA.References.Add(nodeB);
            nodeB.References.Add(nodeA);
            nodeRepository.Add(nodeA);

            IList<ICircle> circles = circlesService.FindCircles(nodeA);
            Assert.IsNotNull(circles, "Even when there is no circle, there should be a valid list");
            Assert.AreEqual(1, circles.Count, "list should have two circles");
        }

        [TestMethod]
        public void ThreeNode()
        {
            var circlesService = new CirclesService();
            var nodeRepository = new NodeRepository();
            var nodeA = new Node("a");
            var nodeB = new Node("b");
            var nodeC = new Node("c");
            nodeA.References.Add(nodeB);
            nodeB.References.Add(nodeC);
            nodeC.References.Add(nodeA);
            nodeRepository.Add(nodeA);

            IList<ICircle> circles = circlesService.FindCircles( nodeA);
            Assert.IsNotNull(circles, "Even when there is no circle, there should be a valid list");
            Assert.AreEqual(1, circles.Count, "list should have two circles");
            var circle=circles[0];
            Assert.IsTrue(circle.Nodes.Contains(nodeA));
            Assert.IsTrue(circle.Nodes.Contains(nodeB));
            Assert.IsTrue(circle.Nodes.Contains(nodeC));
        }

        [TestMethod]
        public void ManyNode()
        {
            var circlesService = new CirclesService();
            var nodeRepository = new NodeRepository();
            var nodeA = new Node("a");
            var nodeB = new Node("b");
            var nodeC = new Node("c");
            var nodeD = new Node("d");
            var nodeE = new Node("e");
            var nodeF = new Node("f");
            var nodeG = new Node("g");
            var nodeH = new Node("h");
            nodeA.Add(nodeC).Add(nodeG);
            nodeB.Add(nodeC).Add(nodeH);
            nodeC.Add(nodeE);
            nodeD.Add(nodeA).Add(nodeB);
            nodeE.Add(nodeD);
            nodeF.Add(nodeE);
            nodeH.Add(nodeA);

            IList<ICircle> circles = circlesService.FindCircles(nodeE);
            Assert.IsNotNull(circles, "Even when there is no circle, there should be a valid list");
            Assert.AreEqual(3, circles.Count, "list should have two circles");
            Assert.AreEqual("e,d,a,c,e",circles[0].ToString());
            Assert.AreEqual("e,d,b,c,e", circles[1].ToString());
            Assert.AreEqual("e,d,b,h,a,c,e", circles[2].ToString());

        }
        [TestMethod]
        public void NodeWithNoReference()
        {
            var nodeA = new Node("a");
            Assert.AreEqual(0, nodeA.References.Count, "no reference");
        }

        [TestMethod]
        public void CompareNodesShouldBeSame()
        {
            var nodeA = new Node("a");
            var nodeB = new Node("a");
            Assert.AreEqual(nodeA, nodeB);
        }
    }
}
