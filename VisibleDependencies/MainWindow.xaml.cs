using Stevpet.Tools.Build;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisibleDependencies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var grid = this.Content as Grid;

            // or any controls
            var fillBrush = new SolidColorBrush(Colors.Gray);
            fillBrush.Opacity = 0.01d;

            var solutionsGrid = new SolutionsGrid();
            solutionsGrid.AddSolution("bla",null, null);
            solutionsGrid.AddSolution("booh", null,null);
            solutionsGrid.AddSolution("jehah", null, null);
            grid.Children.Add(solutionsGrid.Grid);
            Rectangle solution = new Rectangle
            {
                MinWidth = 40,
                MinHeight = 40,
                Width = 100,
                Height = 200,
                Fill = fillBrush,
                Stroke = new SolidColorBrush(Colors.Black),

            Margin = new Thickness(54, 54, 0, 0)
            };
            grid.Children.Add(solution);
        }

        public void GetCircles()
        {
            var artifactRepository = new NodeRepository();

            var solutionRepository = new NodeRepository();

            var finder = new GraphBuilder(solutionRepository, artifactRepository);
            var circleFormatter = new CircleFormattingService(new NodeFormattingService());

            var solutionPaths = Directory.GetFiles("E:/Development/Radiant/Main", "*.sln", SearchOption.AllDirectories);
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
