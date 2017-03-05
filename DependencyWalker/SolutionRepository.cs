using System;
using System.Collections.Generic;

namespace DependencyWalker
{
    public class SolutionRepository
    {
        public SolutionRepository()
        {
            Solutions = new List<SolutionExtract>();
        }
        public void Add(SolutionExtract solution)
        {
            Solutions.Add(solution);
        }

        public IList<SolutionExtract> Solutions { get; private set; }

    }
}