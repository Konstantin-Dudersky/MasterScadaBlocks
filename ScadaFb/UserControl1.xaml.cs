using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ScadaFb
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draws the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="perc">The perc.</param>
        /// <param name="colors">The colors.</param>
        public void Draw(double x, double y, double radius, double[] perc, string[] colors)
        {
            List<Point> points = new List<Point>
            {
                new Point(x + radius, y)
            };

            double sum = 0;

            foreach (double p in perc)
            {
                sum += p;
                points.Add(new Point(x + radius * Math.Cos(2 * Math.PI * sum / 100), 
                    y + radius * Math.Sin(2 * Math.PI * sum / 100)));
            }

            canvas.Children.Clear();

            for (int i = 1; i < points.Count; i++)
            {
                
                PathFigure myPathFigure = new PathFigure
                {
                    StartPoint = new Point(x, y),
                    IsClosed = true,
                };
                myPathFigure.Segments.Add(new LineSegment(points[i - 1], true));
                myPathFigure.Segments.Add(new ArcSegment(points[i], new Size(radius, radius), 0, false, SweepDirection.Clockwise, true));

                PathGeometry myPathGeometry = new PathGeometry();
                myPathGeometry.Figures.Add(myPathFigure);

                Path myPath = new Path
                {
                    //Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Data = myPathGeometry,
                    Fill = new SolidColorBrush(Color.FromArgb(255, 
                        (byte)Convert.ToInt32(colors[i - 1].Substring(2, 2), 16),
                        (byte)Convert.ToInt32(colors[i - 1].Substring(4, 2), 16),
                        (byte)Convert.ToInt32(colors[i - 1].Substring(6, 2), 16))),
                };

                canvas.Children.Add(myPath);
            }
            
        }
    }
}