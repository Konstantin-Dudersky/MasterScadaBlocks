using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MasterScadaBlocks.Pie
{
    public class DiagItem : IComparable<DiagItem>
    {
        public const double LEGENDHEIGHT = 16;

        public double Value { get; set; }
        public string Name { get; }
        public Color Color { get; }
        public double Percent { get; private set; }

        public DiagItem(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public int CompareTo(DiagItem other)
        {
            return Value.CompareTo(other.Value);
        }

        public void SetPercent(double total)
        {
            Percent = 100 * Value / total;
        }

        public Path Segment(double center_x, double center_y, double radius, double angle)
        {
            PathFigure myPathFigure = new PathFigure
            {
                StartPoint = new Point(center_x, center_y),
                IsClosed = true,
            };
            myPathFigure.Segments.Add(new LineSegment(new Point(center_x + radius, center_y), true));
            myPathFigure.Segments.Add(new ArcSegment(new Point(center_x + radius * Math.Cos(2 * Math.PI * Percent / 100),
                    center_y + radius * Math.Sin(2 * Math.PI * Percent / 100)),
                new Size(radius, radius),
                0,
                Percent > 50,
                SweepDirection.Clockwise,
                true));

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(myPathFigure);

            return new Path
            {
                Data = myPathGeometry,
                Fill = new SolidColorBrush(Color),
                RenderTransform = new RotateTransform(angle, center_x, center_y),
            };
        }

        public Shape LegendRect(double left, double top)
        {
            return new Rectangle()
            {
                Width = LEGENDHEIGHT,
                Height = LEGENDHEIGHT,
                Fill = new SolidColorBrush(Color),
                Margin = new Thickness(left, top, 0, 0),
            };
        }

        public Label LegendName(double left, double top)
        {
            return new Label
            {
                Content = Name,
                Height = LEGENDHEIGHT,
                Margin = new Thickness(left, top, 0, 0),
                Padding = new Thickness(0),
                VerticalContentAlignment = VerticalAlignment.Center,
            };
        }

        public Label LegendPercent(double left, double top)
        {
            return new Label
            {
                Content = $"{Percent:0.#} %",
                Height = LEGENDHEIGHT,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(left, top, 0, 0),
                Padding = new Thickness(0),
                VerticalContentAlignment = VerticalAlignment.Center,
                Width = 40
            };
        }
    }
}