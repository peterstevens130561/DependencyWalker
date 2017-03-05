using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyWalker
{
    class DependencyGraphBuilder
    {
        private readonly ISet<SolutionNode> nodes = new HashSet<SolutionNode>();
        public void AddSolution (SolutionNode solutionNode)
        {

            nodes.ToList().ForEach(sn =>
           {
               if(sn.Dependencies.Contains(solutionNode) )
               {
                   solutionNode.Dependents.Add(sn);
                   sn.Dependencies.Add(solutionNode);
               }
               nodes.Add(solutionNode);
           });
        }

        public void IndexNodes()
        {

        }
    }
}
