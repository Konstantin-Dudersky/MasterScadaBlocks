using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MasterScadaBlocks.Flow
{
    public class NodeLink : IComparable<NodeLink>
    {
        public NodeLink(Node input, Node output, double value)
        {
            Input = input;
            Output = output;
            Value = value;
        }

        public Node Input { get; set; }
        public Node Output { get; set; }
        public double Value { get; set; }
        private double X1 { get; set; }
        private double X2 { get; set; }
        private double Y11 { get; set; }
        private double Y12 { get; set; }
        private double Y21 { get; set; }
        private double Y22 { get; set; }

        public static bool operator <(NodeLink nl1, NodeLink nl2)
        {
            return (nl1.CompareTo(nl2) < 0);
        }

        public static bool operator >(NodeLink nl1, NodeLink nl2)
        {
            return (nl1.CompareTo(nl2) > 0);
        }

        public int CompareTo(NodeLink other)
        {
            //return Y11.CompareTo(other.Y11);
            return (Y11 + Y21).CompareTo(other.Y11 + other.Y21);
        }

        public void Paint(Canvas canvas)
        {
            double xBez1 = X1 + 0.25 * (X2 - X1);
            double xBez2 = X2 - 0.25 * (X2 - X1);

            double yTopMiddle = (Y21 - Y11) / 2 + Y11;

            PathFigure pathFigure = new PathFigure()
            {
                StartPoint = new Point(X1, Y11),
                IsClosed = true,
            };

            pathFigure.Segments.Add(new BezierSegment(
                new Point(xBez1, Y11),
                new Point(xBez2, Y21),
                new Point(X2, Y21),
                true));

            pathFigure.Segments.Add(new LineSegment(new Point(X2, Y22), true));

            pathFigure.Segments.Add(new BezierSegment(
                new Point(xBez2, Y22),
                new Point(xBez1, Y12),
                new Point(X1, Y12),
                true));

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(pathFigure);

            LinearGradientBrush linearGradient = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(1, 0.5),
            };
            linearGradient.GradientStops.Add(new GradientStop(Color.FromArgb((byte)(0.4 * Input.Color.A), Input.Color.R, Input.Color.G, Input.Color.B), 0.0));
            linearGradient.GradientStops.Add(new GradientStop(Color.FromArgb((byte)(0.4 * Output.Color.A), Output.Color.R, Output.Color.G, Output.Color.B), 1.0));

            Path path = new Path
            {
                Data = myPathGeometry,
                Fill = linearGradient,
            };

            canvas.Children.Add(path);
        }

        public void SetPosition(double yCoef)
        {
            X1 = Input.MarginLeft + Input.SizeWidth;
            X2 = Output.MarginLeft;

            Y11 = Input.MarginTop + Input.OutputUsed * yCoef;
            Y21 = Output.MarginTop + Output.InputUsed * yCoef;

            Input.OutputUsed += Value;
            Output.InputUsed += Value;

            Y22 = Output.MarginTop + Output.InputUsed * yCoef;
            Y12 = Input.MarginTop + Input.OutputUsed * yCoef;
        }

        public override string ToString()
        {
            return $"Input = {Input.Name}, Output = {Output.Name}, Value = {Value}, Y11 = {Y11:f1}";
        }

        static public int CompareByBegin(NodeLink link1, NodeLink link2)
        {
            return link1.Y11.CompareTo(link2.Y11);
        }

        static public int CompareByEnd(NodeLink link1, NodeLink link2)
        {
            return link1.Y21.CompareTo(link2.Y21);
        }
    }
}