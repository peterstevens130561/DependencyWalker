using System;
using System.Collections.Generic;

namespace DependencyWalker
{
    /// <summary>
    /// Extract of the information in the SolutionFile. 
    /// </summary>
    public class SolutionExtract
    {

        public SolutionExtract(string solutionLocation)
        {
            Location = solutionLocation;
            Artifacts = new List<string>();
            References = new List<string>();
        }

        public SolutionExtract(string solutionLocation, IList<string> references, IList<string> artifacts) : this(solutionLocation)
        {
            Location = solutionLocation;
            References= references;
            Artifacts = artifacts;
        }

        public IList<string> Artifacts { get; private set; }
        public IList<string> References { get; private set; }
        public string Location { get; internal set; }

        new int GetHashCode()
        {
            return Location.GetHashCode();
        }

        new Boolean Equals(object o)
        {
            var other = o as SolutionExtract;
            return other == null || other.Location != Location;
        }
    }
}