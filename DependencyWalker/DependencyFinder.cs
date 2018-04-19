using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using System.IO;

namespace DependencyWalker
{
    class DependencyFinder
    {
        private readonly ArtifactRepository artifactRepository;
        private readonly ReferenceRepository referenceRepository;
        private readonly SolutionRepository solutionRepository;
        private string solutionLocation;

        public DependencyFinder(SolutionRepository solutionRepository,ArtifactRepository artifactRepository, ReferenceRepository referenceRepository)
        {
            this.solutionRepository = solutionRepository;
            this.artifactRepository = artifactRepository;
            this.referenceRepository = referenceRepository;
        }
        public void Solution(String solutionLocation)
        {

            this.solutionLocation = solutionLocation;
            IList<string> artifacts = new List<string>();
            var solutionReferences = new List<string>();
            var solutionFile = SolutionFile.Parse(solutionLocation);
            var projectsInSolution = solutionFile.ProjectsInOrder;
            projectsInSolution.ToList().ForEach(p =>
           {
               if (p.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat)
               {
                   var solutionFolder = Path.GetFullPath(solutionLocation);
                   var projectLocation = Path.Combine(Path.GetDirectoryName(solutionFolder), p.RelativePath);
                   if (File.Exists(projectLocation))
                   {
                       var project = new Project(projectLocation);

                       string assemblyName = GetAssemblyName(p, project);
                       if (assemblyName != null)
                       {
                           var artifact = new Artifact(solutionLocation, project.FullPath, assemblyName);
                           artifacts.Add(assemblyName);
                           artifactRepository.Add(artifact);
                       }
                       var projectReferences = GetProjectReferences(solutionLocation, project);
                       solutionReferences.AddRange(projectReferences); // this may be wrong

                       ProjectCollection.GlobalProjectCollection.UnloadProject(project);
                   }
               }

           });
            var solution = new SolutionExtract(solutionLocation, solutionReferences, artifacts);
            solutionRepository.Add(solution);
        }

  
        private IList<string> GetProjectReferences(string solutionLocation,Project project)
        {
            IList<string> references = new List<string>();
            project.Xml.ItemGroups.ToList().ForEach(ige =>
            {
                ige.Children
                 .Where(child => ((ProjectItemElement)child).ItemType == "Reference").ToList()
                     .ForEach(r =>
                     {
                         var referenceElement = (ProjectItemElement)r;
                         string include = referenceElement.Include;
                         var reference = new Reference(solutionLocation, referenceElement.ContainingProject.ProjectFileLocation.LocationString, include);
                         referenceRepository.Add(reference);
                         references.Add(include);
                     });
            });
            return references;
        }

        private static string GetAssemblyName(ProjectInSolution p, Project project)
        {
            string assemblyName = null;
            if (p.RelativePath.EndsWith(".vcxproj"))
            {
                assemblyName = p.ProjectName;
            }
            else
            {
                var assemblyElement = project.Xml.Properties.FirstOrDefault(pe => pe.Name == "AssemblyName");
                if (assemblyElement != null)
                {
                    assemblyName = assemblyElement.Value;
                }
            }

            return assemblyName;
        }

        private void AppendReference(ProjectItemElement projectReference)
        {
            var reference = new Reference(solutionLocation, projectReference.ContainingProject.ProjectFileLocation.LocationString, projectReference.Include);
            referenceRepository.Add(reference);
                Console.WriteLine(projectReference.ContainingProject.ProjectFileLocation + "|" + projectReference.Include);
        }
    }

}
