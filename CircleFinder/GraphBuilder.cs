﻿
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    class GraphBuilder
    {
        private string solutionLocation;
        private readonly NodeRepository artifactRepository;
        private readonly NodeRepository solutionRepository;

        public GraphBuilder(NodeRepository solutionRepository,NodeRepository artifactRepository)
        {
            this.solutionRepository = solutionRepository;
            this.artifactRepository = artifactRepository;
        }
        public void BuildArtifactsOfSolution(String solutionLocation)
        {

            this.solutionLocation = solutionLocation;
            var solutionFile = SolutionFile.Parse(solutionLocation);
            var projectsInSolution = solutionFile.ProjectsInOrder;
            var solutionNode = new SolutionNode(solutionLocation);
            solutionRepository.Add(solutionNode);
            projectsInSolution.ToList().ForEach(p =>
            {
                Console.WriteLine(p.RelativePath);
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
                            var artifact=solutionNode.CreatesArtifact(assemblyName);
                            artifactRepository.Add(artifact);
                        }

                        ProjectCollection.GlobalProjectCollection.UnloadProject(project);
                    }
                }

            });
            Console.WriteLine("Done");
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
               var dependencyNode = (ArtifactNode)artifactRepository.GetByName(dependency);
               if (dependencyNode !=null)
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
