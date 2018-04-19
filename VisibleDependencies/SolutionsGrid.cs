using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisibleDependencies
{
    class SolutionsGrid
    {
        int solutions = 0;

        public SolutionsGrid()
        {
            Grid = new Grid();
            Grid.ShowGridLines = false;
        }

        public void AddSolution(String name, IList<String> projects, Grid solution)
        {
            var solutionRowDefinition = new RowDefinition
            {
                Height = new System.Windows.GridLength(50, System.Windows.GridUnitType.Auto)
            };

            var border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Blue);
            border.BorderThickness = new Thickness(1);
            Grid.Children.Add(border);
            Grid.SetRow(border, solutions);
            Grid.RowDefinitions.Add(solutionRowDefinition);


            var solutionGrid = new Grid();
            Grid.SetRow(solutionGrid, solutions);
            Grid.Children.Add(solutionGrid);

            RowDefinition labelRow = CreateLabelRow();
            solutionGrid.RowDefinitions.Add(labelRow);

            RowDefinition solutionRow = CreateSolutionRow();
            solutionGrid.RowDefinitions.Add(solutionRow);

            RowDefinition bottomRow = CreateBottomRow();
            solutionGrid.RowDefinitions.Add(bottomRow);

            var label = new Label
            {
                Content = name,
                Margin = new Thickness(0, 0, 0, 0)
            };
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, 0);
            solutionGrid.Children.Add(label);

            //we'll do this for each element in the circle
            Rectangle solutionRectangle = CreateSolutionRectangle();
            Grid.SetRow(solutionRectangle, 1);
            Grid.SetColumn(solutionRectangle, 0);

            solutionGrid.Children.Add(solutionRectangle);


            solutions++;
            // we'll do this for each element in the circle
            Line line = CreateConnectingLine(solutionRectangle);
            solutionGrid.Children.Add(line);
        }

        private static RowDefinition CreateBottomRow()
        {
            return new RowDefinition
            {
                Height = new System.Windows.GridLength(30, System.Windows.GridUnitType.Pixel)
            };
        }

        private static RowDefinition CreateSolutionRow()
        {
            return new RowDefinition
            {
                Height = new System.Windows.GridLength(50, System.Windows.GridUnitType.Auto)

            };
        }

        private static RowDefinition CreateLabelRow()
        {
            return new RowDefinition
            {
                Height = new System.Windows.GridLength(30, System.Windows.GridUnitType.Pixel)
            };
        }

        private Rectangle CreateSolutionRectangle()
        {
            return new Rectangle
            {
                MinWidth = 40,
                MinHeight = 40,
                Width = 100,
                Height = 200 - solutions * 20,
                Stroke = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(1, 0, 0, 0)
            };
        }

        private static Line CreateConnectingLine(Rectangle solutionRectangle)
        {
            var line = new Line
            {
                X1 = solutionRectangle.Margin.Left + solutionRectangle.Width,
                Y1 = solutionRectangle.Margin.Top + 10,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            };
            Grid.SetRow(line, 1);
            Grid.SetColumn(line, 0);
            line.X2 = line.X1 + 20;
            line.Y2 = line.Y1;
            return line;
        }

        public Grid Grid { private set;  get; }
    }
}
