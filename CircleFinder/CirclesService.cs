using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class CirclesService
    {
        /// <summary>
        /// find all unique circles of which this node is an element
        /// </summary>
        /// <param name="searchNode"></param>
        /// <returns></returns>
        public IList<ICircle> FindCircles(Node searchNode)
        {
            var result = new List<ICircle>();
            searchNode.Children.ToList().ForEach(currentNode =>
            {
                StartSearch(searchNode, currentNode, result);
            });
            return result;
        }



        public IList<ICircle> FindCircles(NodeRepository nodeRepository)
        {
            IList<ICircle> result = null;
            nodeRepository.Nodes.ToList().ForEach(n =>
            {
               result = new List<ICircle>();
                n.Children.ToList().ForEach(a =>
               {
                   StartSearch(n, a, result);
               });

           });
            return result;
        }

        private static void StartSearch(Node searchNode, Node currentNode, IList<ICircle> result)
        {
            Search(searchNode, currentNode, result, new List<Node>(), new List<Node>());
        }

        /// <summary>
        /// Search recursively from startNode for searchNode, adding each found circle to foundCircles.
        /// 
        ///
        /// </summary>
        /// <param name="searchNode"></param>
        /// <param name="startNode"></param>
        /// <param name="foundcircles"></param>
        /// <param name="path">The p</param>
        /// <param name="traversed">The nodes encountered thusfar</param>
        private static void Search(Node searchNode, Node startNode, IList<ICircle> foundcircles, IList<Node> path,IList<Node>traversed)
        {
            path.Add(startNode);

            foreach (Node currentNode in startNode.Children.Where(node => !traversed.Contains(node)))
            {
                traversed.Add(currentNode);
                //The Match is overriden by the artifact to check on the solution....
                if (currentNode.Matches(searchNode) && path.Any(n => !n.Matches(searchNode)))
                {
                    Circle circle = new Circle();
                    path.ToList().ForEach(node => circle.Add(node));
                    circle.Add(currentNode);
                    foundcircles.Add(circle);
                }
                else if (path.Contains(currentNode))
                {
                    // detected another circle
                }
                else
                {
                    Search(searchNode, currentNode, foundcircles, path,traversed);
                }
            }
            path.Remove(startNode);
        }
    }
}
