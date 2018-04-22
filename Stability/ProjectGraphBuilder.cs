using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    class ProjectGraphBuilder
    {
        public class GraphBuilder
        {
            private string _solutionLocation;
            private readonly NodeRepository _artifactRepository;
            private readonly NodeRepository _solutionRepository;

            public GraphBuilder(NodeRepository solutionRepository, NodeRepository artifactRepository)
            {
                this._solutionRepository = solutionRepository;
                this._artifactRepository = artifactRepository;
            }
            public void BuildArtifactsOfSolution(String solutionLocation)
            {

                this._solutionLocation = solutionLocation;
                var solutionFile = SolutionFile.Parse(solutionLocation);
                var projectsInSolution = solutionFile.ProjectsInOrder;
                var solutionFolder = Path.GetFullPath(solutionLocation);
                projectsInSolution.Where(p => p.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat).ToList().ForEach(p =>
                {
                    var projectLocation = Path.Combine(Path.GetDirectoryName(solutionFolder), p.RelativePath);
                    if (File.Exists(projectLocation))
                    {
                        var project = new Project(projectLocation);
                        var items = project.GetItems("ProjectReference");
                        items.ToList().ForEach(i => { i.EvaluatedInclude});
                        string assemblyName = GetAssemblyName(p, project);
                        if (assemblyName != null)
                        {
                        }

                        ProjectCollection.GlobalProjectCollection.UnloadProject(project);
                        }

                });
            }

            /// <summary>
            /// Resolve the dependencies for this artifact
            /// </summary>
            /// <param name="artifactNode"></param>
            public void ResolveDependencies(ArtifactNode artifactNode)
            {
                var project = new Project(artifactNode.ProjectLocation);
                var dependencies = GetDependencies(project);
                dependencies.ToList().ForEach(dependency =>
                {
                    var dependencyNode = (ArtifactNode)_artifactRepository.GetByName(dependency);
                    if (dependencyNode != null)
                    {
                        artifactNode.DependsOn(dependencyNode);
                    }
                });
                ProjectCollection.GlobalProjectCollection.UnloadProject(project);

            }

            private IList<string> GetDependencies(Project project)
            {
                IList<string> references = new List<string>();
                project.Xml.ItemGroups.ToList().ForEach(ige =>
                {
                    ige.Children
                     .Where(child => ((ProjectItemElement)child).ItemType == "Reference").ToList()
                         .ForEach(r =>
                         {
                             var referenceElement = (ProjectItemElement)r;
                             references.Add(referenceElement.Include);
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
        }
    }

}
}
