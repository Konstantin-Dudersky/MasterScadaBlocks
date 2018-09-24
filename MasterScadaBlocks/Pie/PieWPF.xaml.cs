using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace MasterScadaBlocks.Pie
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PieWPF : UserControl
    {
        private int ControlWidth, ControlHeight;

        public PieWPF(int width, int height)
        {
            InitializeComponent();
            ControlWidth = width;
            ControlHeight = height;
        }

        public void Draw(double x, double y, double radius, double[] perc, List<Color> colors, DiagItemCollection data)
        {
            canvas.Children.Clear();

            data.Draw(canvas);
        }
    }
}