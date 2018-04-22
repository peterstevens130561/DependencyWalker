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
        private static TestContext _testContext;
        private int _projectsFound;
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _testContext = context;
        }
        [TestMethod]
        public void Execute_OneProject_StabilityIsOne()
        {
            var asm = Assembly.GetExecutingAssembly();
            string resourcePath = Path.Combine(Directory.GetParent(new Uri(asm.CodeBase).LocalPath).FullName, "Resources");
            ISolutionParser parser = new SolutionParser();
            string solutionPath = Path.Combine(resourcePath, "SimpleSolution.sln");
            parser.OnProject += OnProject;
            parser.Parse(solutionPath);
            parser.OnProject -= OnProject;
            Assert.AreEqual(6, _projectsFound);
            
        }

        private void OnProject(IProjectInSolution solutionProject)
        {I
            ++_projectsFound;
        }


    }
}
