using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stevpet.Tools.Build;

namespace BHI.ArchitectureTools.StabilityCalculator.UnitTests
{
    [TestClass]
    public class StabilityCalculatorUnitTests
    {
        private int _projectsFound;
        private ISolutionParser parser;
    
        [TestInitialize]
        public void TestInitialize()
        {
            var asm = Assembly.GetExecutingAssembly();
            string resourcePath = Path.Combine(Directory.GetParent(new Uri(asm.CodeBase).LocalPath).FullName, "Resources");
            parser = new SolutionParser();
            string solutionPath = Path.Combine(resourcePath, "SimpleSolution.sln");
            parser.OnProject += OnProject;
            parser.Parse(solutionPath);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            parser.OnProject -= OnProject;
        }
        [TestMethod]
        public void Parse_SimpleSolution_SixProjectsFound()
        {
            Assert.AreEqual(6, _projectsFound,"the solution has 6 projects");  
        }

        public void Parse_SimpleSolution_FirstProject()
        {
            Assert.AreEqual(6, _projectsFound, "the solution has 6 projects");
        }

        public void Parse_SimpleSolution_FirstProjectNameIs
        private void OnProject(IProjectInSolution solutionProject)
        {
            ++_projectsFound;
        }


    }
}
