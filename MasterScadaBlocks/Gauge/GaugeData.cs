using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MasterScadaBlocks.Gauge
{
    public class GaugeData
    {
        public double Min { get; set; }
                public double LL { get; set; }
        public double L { get; set; }
        public double H { get; set; }
        public double HH { get; set; }
                public double Max { get; set; }

        private double value_field;
        public double Value {
            get => value_field;
            set {
                if (value > Max)
                {
                    value_field = Max;
                }
                else if (value < Min)
                {
                    value_field = Min;
                }
                else
                {
                    value_field = value;
                }
            }
        }
        public string Header { get; set; }


        private const double RADIUS = 0.4 * WIDTH;
        private const double CENTER_X = WIDTH / 2;
        private const double CENTER_Y = 200;
        private const double OFFSET_COEF = 0.7;
        private const double WIDTH = 400;

        private readonly Color COLOR_VALUE = Color.FromArgb(255, 173, 178, 35);
        private readonly Color COLOR_LIGHT = Color.FromArgb(180, 255, 255, 255);
        private readonly Color COLOR_WARNING = Color.FromArgb(255, 237, 187, 32);
        private readonly Color COLOR_ALARM = Color.FromArgb(255, 150, 53, 95);
        private readonly Color COLOR_TEXT = Color.FromArgb(255, 35, 55, 70);

        public void Paint(Canvas canvas)
        {
            canvas.Children.Clear();

            canvas.Children.Add(Segment(CENTER_X, CENTER_Y, RADIUS, Min, Max, COLOR_VALUE));

            canvas.Children.Add(Segment(CENTER_X, CENTER_Y, RADIUS, H, Max, COLOR_WARNING));
            canvas.Children.Add(Segment(CENTER_X, CENTER_Y, RADIUS, Min, L, COLOR_WARNING));

            canvas.Children.Add(Segment(CENTER_X, CENTER_Y, RADIUS, HH, Max, COLOR_ALARM));
            canvas.Children.Add(Segment(CENTER_X, CENTER_Y, RADIUS, Min, LL, COLOR_ALARM));

            canvas.Children.Add(Segment(CENTER_X - 0.8 * (1 - OFFSET_COEF) * RADIUS, CENTER_Y, OFFSET_COEF * RADIUS, Min, Max, COLOR_LIGHT));

            canvas.Children.Add(Arrow(CENTER_X, CENTER_Y, RADIUS, Value));

            double labelWidth = 60;

            /// Минимум
            canvas.Children.Add(new Label()
            {
                Content = $"{Min:f1}",
                Foreground = new SolidColorBrush(COLOR_TEXT),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(CENTER_X - RADIUS, CENTER_Y, 0, 0),
                VerticalContentAlignment = VerticalAlignment.Top,
                Width = labelWidth,
            });

            /// Максимум
            canvas.Children.Add(new Label()
            {
                Content = $"{Max:f1}",
                Foreground = new SolidColorBrush(COLOR_TEXT),
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(CENTER_X + RADIUS - labelWidth, CENTER_Y, 0, 0),
                VerticalContentAlignment = VerticalAlignment.Top,
                Width = labelWidth,
            });

            /// Заголовок
            canvas.Children.Add(new Label()
            {
                Content = Header,
                Foreground = new SolidColorBrush(COLOR_TEXT),
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Width = WIDTH,
            });

            /// Показания
            canvas.Children.Add(new Label()
            {
                Content = $"{Value:f2}",
                Foreground = new SolidColorBrush(COLOR_TEXT),
                FontSize = 20,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 1.1 * CENTER_Y, 0, 0),
                Width = WIDTH,
            });
        }

        private double Angle(double value)
        {
            return Math.PI * (value - Min) / (Max - Min);
        }

        private Path Segment(double centerX, double centerY, double radius, double startValue, double endValue, Color color)
        {
            if (startValue > endValue)
            {
                return new Path();
            }

            PathFigure pathFigure = new PathFigure()
            {
                StartPoint = new Point(centerX, centerY),
                IsClosed = true,
            };

            pathFigure.Segments.Add(new LineSegment(new Point(centerX - radius * Math.Cos(Angle(startValue)),
                centerY - radius * Math.Sin(Angle(startValue))), true));
            pathFigure.Segments.Add(new ArcSegment(new Point(centerX - radius * Math.Cos(Angle(endValue)),
                centerY - radius * Math.Sin(Angle(endValue))),
                new Size(radius, radius),
                0,
                false,
                SweepDirection.Clockwise,
                true));

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(pathFigure);

            Path path = new Path
            {
                Data = myPathGeometry,
                Fill = new SolidColorBrush(color),
            };

            return path;
        }

        private Path Arrow(double centerX, double centerY, double radius, double value)
        {
            double radius_base = 0.1 * radius;

            PathFigure pathFigure = new PathFigure()
            {
                StartPoint = new Point(centerX, centerY - radius_base),
                IsClosed = true,
            };

            pathFigure.Segments.Add(new ArcSegment(new Point(centerX, centerY + radius_base),
                new Size(radius_base, radius_base),
                0,
                false,
                SweepDirection.Clockwise,
                true));

            pathFigure.Segments.Add(new LineSegment(new Point(centerX - 0.85 * radius, centerY), true));

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(pathFigure);

            RotateTransform rotate = new RotateTransform(180 * Angle(value) / Math.PI, centerX, centerY);

            Path path = new Path
            {
                Data = myPathGeometry,
                Fill = new SolidColorBrush(Color.FromArgb(255, 35, 55, 70)),
                RenderTransform = rotate,
                Stroke = Brushes.White,
                StrokeThickness = 2,
            };

            return path;
        }
    }
}