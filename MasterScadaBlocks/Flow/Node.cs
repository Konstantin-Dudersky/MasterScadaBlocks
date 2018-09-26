using MasterScadaBlocksCommon;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MasterScadaBlocks.Flow
{
    public class Node
    {
        #region Private Fields

        private const int MINLABELHEIGHT = 16;

        #endregion Private Fields

        #region Public Constructors

        public Node(string name, Color color)
        {
            Name = name;
            Level = null;
            OrderInLevel = null;
            InputUsed = 0;
            OutputUsed = 0;
            Color = color;
            ColorDarker = CommonClass.ChangeLuminosity(Color, -0.2);
        }

        #endregion Public Constructors



        #region Public Properties

        public Color Color { get; private set; }
        public double InputUsed { get; set; }
        public int? Level { get; set; }
        public double MarginLeft { get; private set; }
        public double MarginTop { get; private set; }
        public string Name { get; set; }
        public int? OrderInLevel { get; set; }
        public double OutputUsed { get; set; }
        public double SizeHeight { get; private set; }
        public double SizeWidth { get; private set; }
        public double Value { get; set; }
        public double ValuePercent { get; set; }

        #endregion Public Properties



        #region Private Properties

        private Color ColorDarker { get; set; }

        #endregion Private Properties



        #region Public Methods

        public void Paint(Canvas canvas)
        {
            if (Value == 0)
            {
                return;
            }

            // прямоугольник
            canvas.Children.Add(new Rectangle()
            {
                Margin = new Thickness(MarginLeft, MarginTop, 0, 0),
                Width = SizeWidth,
                Height = SizeHeight,
                Fill = new SolidColorBrush(Color),
                Stroke = new SolidColorBrush(ColorDarker),
            });

            // подпись
            canvas.Children.Add(new Label()
            {
                Content = SizeHeight < MINLABELHEIGHT ? $"{Name} ({ValuePercent:f1} %)" : $"{Name}\n{ValuePercent:f1} %",
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(ColorDarker),
                Height = Math.Max(SizeHeight, MINLABELHEIGHT),
                Margin = new Thickness(MarginLeft + SizeWidth + 2, MarginTop, 0, 0),
                Padding = new Thickness(0),
                VerticalContentAlignment = SizeHeight < MINLABELHEIGHT ? VerticalAlignment.Top : VerticalAlignment.Center,
            });
        }

        public void SetPosition(double xCoef, double yCoef, double topOffset)
        {
            SizeHeight = Value * yCoef;
            SizeWidth = 20;
            MarginLeft = (Level ?? 0) * xCoef;
            System.Console.WriteLine(MarginLeft.ToString());
            MarginTop = topOffset * yCoef;
        }

        public override string ToString()
        {
            return $"Name = {Name}, Level = {Level}, Value = {Value}, OrderInLevel = {OrderInLevel}";
        }

        #endregion Public Methods
    }
}