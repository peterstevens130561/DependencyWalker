using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stevpet.Tools.Build;

namespace BHI.ArchitectureTools.StabilityCalculator.UnitTests
{
    [TestClass]
    public class StabilityCalculatorUnitTests
    {
        private int _projectsFound;
        private ISolutionParser parser;
        private IList<IProjectInSolution> projects = new List<IProjectInSolution>();
        private string _resourcesPath;
        [TestInitialize]
        public void TestInitialize()
        {
            var asm = Assembly.GetExecutingAssembly();
            _resourcesPath = Path.Combine(Directory.GetParent(new Uri(asm.CodeBase).LocalPath).FullName, "Resources");
            parser = new SolutionParser();
            string solutionPath = Path.Combine(_resourcesPath, "SimpleSolution.sln");
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

        [TestMethod]
        public void Parse_SimpleSolution_FirstAssemblyNameProjectNameIsDependencyWalker()
        {
            Assert.AreEqual("DependencyWalker", projects[0].AssemblyName);
        }
        [TestMethod]
        public void Parse_SimpleSolution_SecondAssemblyNameProjectNameIsUnitTestProject1()
        {
            Assert.AreEqual("UnitTestProject1", projects[1].AssemblyName);
        }

        [TestMethod]
        public void Parse_SimpleSolution_SecondProjectFileMatches()
        {
            string expectedPath = Path.Combine(_resourcesPath, @"UnitTestProject1\DependencyWalker.UnitTests.csproj");
            Assert.AreEqual(expectedPath, projects[1].ProjectFile);
        }

        [TestMethod]
        public void Parse_SimpleSolution_ThirdAssemblyNameProjectNameIsCircleFinder()
        {
            Assert.AreEqual("CircleFinder", projects[2].AssemblyName);
        }

        private void OnProject(IProjectInSolution project)
        {
            ++_projectsFound;
            projects.Add(project);
        }


    }
}
