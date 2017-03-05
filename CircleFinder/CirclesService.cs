using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class CirclesService
    {
        public IList<ICircle> FindCircles(Node startNode)
        {
            var result = new List<ICircle>();
            var path = new List<Node>();
            Search(startNode, startNode, result, path);
            return result;
        }

        public IList<ICircle> FindCircles(NodeRepository nodeRepository)
        {
            var result = new List<ICircle>();
            nodeRepository.Nodes.ToList().ForEach(n =>
            {
               n.References.ToList().ForEach(a =>
               {
                   Search(n, a, result, new List<Node>());
               });
           });
            return result;
        }
        private static void Search(Node searchNode, Node startNode, IList<ICircle> result, IList<Node> path)
        {
            path.Add(startNode);
            var nextNodes = startNode.References;
            foreach (Node current in nextNodes)
            {
                if (current.Matches(searchNode))
                {
                    Circle circle = new Circle();
                    path.ToList().ForEach(node => circle.Add(node));
                    circle.Add(current);
                    result.Add(circle);
                }
                else if (path.Contains(current))
                {
                    // detected another circle
                }
                else
                {
                    Search(searchNode, current, result, path);
                }
            }
            path.Remove(startNode);
        }
    }
}
