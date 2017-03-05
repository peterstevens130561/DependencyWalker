using System;
using System.Collections.Generic;

namespace DependencyWalker
{
    public class SolutionNode
    {
        public SolutionNode(string solutionPath)
        {
            Location = solutionPath;
            Dependents = new List<SolutionNode>();
            Dependencies = new List<SolutionNode>();
        }

        public SolutionNode(SolutionExtract solution)
        {
            Location = solution.Location;

        }
        /// <summary>
        /// The list of solutions that need this solution
        /// </summary>
        public IList<SolutionNode> Dependents { get; private set; }
        /// <summary>
        /// The list of solutions that are needed by this solution
        /// </summary>
        public IList<SolutionNode> Dependencies { get; private set; }
        public int Index { get; internal set; }
        public string Location { get; private set; }
    }

}