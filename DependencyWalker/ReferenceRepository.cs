using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyWalker
{
    class ReferenceRepository
    {
        // key is the assembly
        private readonly Dictionary<String, IList<Reference>> repository = new Dictionary<String, IList<Reference>>();
        public void Add(Reference reference)
        {
            IList<Reference> projects = GetProjectsReferingTo(reference);
            var inList = projects.Any(r => (r.ProjectLocation == reference.ProjectLocation) && (r.SolutionLocation == reference.SolutionLocation));
            if (!inList)
            {
                projects.Add(reference);
            }
        }

        private IList<Reference> GetProjectsReferingTo(Reference reference)
        {
            if (!repository.ContainsKey(reference.Name))
            {
                repository.Add(reference.Name, new List<Reference>());
            }
            return repository[reference.Name];
        }

        public IList<string> GetSolutionLocationsFor(string a)
        {
            throw new NotImplementedException();
        }
    }
}
