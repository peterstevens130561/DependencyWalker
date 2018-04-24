using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    public class SolutionParser : ISolutionParser
    {
        public Action<IProjectInSolution> OnProject { get; set; }

        public void Parse(string solutionFile)
        {
            var solution = SolutionFile.Parse(solutionFile);
        
            solution.ProjectsInOrder.ToList().ForEach(p =>
            {
                string pathToProject = p.AbsolutePath;
                var project = new Project(pathToProject);

                var assemblyName = project.Properties.First(prop => prop.Name == "AssemblyName").EvaluatedValue;
                var projectInSolution = new ProjectInSolution(assemblyName,pathToProject);
                OnProject.Invoke(projectInSolution);
                ProjectCollection.GlobalProjectCollection.UnloadProject(project);
            }
            );
        }
    }
}
