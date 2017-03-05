using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyWalker
{
    class SolutionServices
    {
        private readonly ReferenceRepository referenceRepository;

        public SolutionServices(ReferenceRepository artifactRepository)
        {
            this.referenceRepository = artifactRepository;
        }

        public IList<string> GetDependentSolutions(IList<String> artifacts)
        {
            ISet<string> solutionLocations = new HashSet<string>();
            artifacts.ToList().ForEach(a =>
           {
               var locations = referenceRepository.GetSolutionLocationsFor(a);
               

           });
            return new List<string>(solutionLocations);
        }
    }
}
