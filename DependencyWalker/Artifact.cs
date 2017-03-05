using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyWalker
{
    class Artifact
    {

        public Artifact(string solutionLocation, string projectLocation, string name)
        {
            SolutionLocation = solutionLocation;
            ProjectLocation = projectLocation;
            Name = name;
        }

        public string Name { get; private set; }
        public string SolutionLocation { get; private set; }
        public string ProjectLocation { get; private set; }

        public string Key {  get { return Name + "|" + SolutionLocation;  } }
    }
}
