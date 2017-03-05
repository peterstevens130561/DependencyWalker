using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DependencyWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            ArtifactRepository artifactRepository = new ArtifactRepository();
            ReferenceRepository referenceRepository = new ReferenceRepository();
            SolutionRepository solutionRepository = new SolutionRepository();
            var finder = new DependencyFinder(solutionRepository,artifactRepository,referenceRepository);
            var solutionPaths = Directory.GetFiles("E:/Development/Radiant/Main", "*.sln", SearchOption.AllDirectories);
            solutionPaths.ToList().ForEach(p => finder.Solution(p));
        }
    }
}
