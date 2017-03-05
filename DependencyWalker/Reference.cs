using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyWalker
{
    class Reference
    {

        /// <summary>
        /// Create a new reference value object
        /// </summary>
        /// <param name="solutionLocation">path</param>
        /// <param name="projectLocation">path</param>
        /// <param name="name">include</param>
        public Reference(string solutionLocation,string projectLocation,string name)
        {
            SolutionLocation = solutionLocation;
            ProjectLocation=projectLocation;
            Name = name;
        }

        public string SolutionLocation { get; private set; }
        public string ProjectLocation { get; private set; }
        public string Name { get; private set; }

        public string Key {  get { return Name + "|" + ProjectLocation; } }
    }
}
