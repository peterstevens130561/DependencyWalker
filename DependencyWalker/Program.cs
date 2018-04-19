using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Stevpet.Tools.Build;

namespace DependencyWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            var artifactRepository = new NodeRepository();

            var solutionRepository = new NodeRepository();

            var finder = new GraphBuilder(solutionRepository, artifactRepository);
            var circleFormatter = new CircleFormattingService(new NodeFormattingService());
            var solutionPaths = Directory.GetFiles(@"E:\Cadence\ESIETooLink\Main", "Bhi.Esie.Toolink.sln", SearchOption.AllDirectories);

            solutionPaths.ToList().ForEach(p => {
                finder.BuildArtifactsOfSolution(p);
            });
            artifactRepository.Nodes.ToList().ForEach(n => {
                finder.ResolveDependencies((ArtifactNode)n);
            });

            // Here we've built the graph, would be nice to show it, wouldn't it

            var circleService = new CirclesService();
            solutionRepository.Nodes.ToList().ForEach(node =>
            {
                var circles = circleService.FindCircles(node);
                if (circles.Any())
                {
                    Console.WriteLine($"{node.Name} {circles.Count()}");
                    circles.ToList().ForEach(circle => Console.WriteLine(circleFormatter.FormatCircle(circle)));
                }

            });
        }
    }
}
