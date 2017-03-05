using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyWalker
{
    class ArtifactRepository
    {
        private readonly Dictionary<String,Artifact> repository = new Dictionary<String,Artifact>();
        public void Add(Artifact artifact)
        {
            if(repository.ContainsKey(artifact.Key))
            {
                return;
            }
            repository.Add(artifact.Key,artifact);
        }

        public Artifact GetByName(string name)
        {
            return repository[name];
        }
    }
}
