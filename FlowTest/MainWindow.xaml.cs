using MasterScadaBlocksCommon;
using MasterScadaBlocks.Flow;
using System.Windows;
using System.Windows.Media;

namespace FlowTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NodeCollection data = new NodeCollection();

            data.AddNode(0, "Net supply", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0));
            data.AddNode(1, "PV", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi1));
            data.AddNode(2, "Factory", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi2));
            data.AddNode(3, "Utilities", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi3));
            data.AddNode(4, "Admin Building", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi4));
            data.AddNode(5, "Production", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi5));
            data.AddNode(6, "Packaging", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi6));
            data.AddNode(7, "Compressor", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi7));
            data.AddNode(8, "HVAC", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi8));
            data.AddNode(9, "Boiler", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi9));

            data.AddNodeLink(0, 4, 3185);
            data.AddNodeLink(0, 2, 57426);
            data.AddNodeLink(0, 3, 4259);
            data.AddNodeLink(1, 2, 2544);
            data.AddNodeLink(2, 5, 42322);
            data.AddNodeLink(2, 6, 15104);
            data.AddNodeLink(3, 7, 704);
            data.AddNodeLink(3, 8, 2316);
            data.AddNodeLink(3, 9, 1239);

            data.AddLevels(0, "0 1");
            data.AddLevels(1, "2 3");
            data.AddLevels(2, "4 5 6 7 8 9");

            textBlock.Text = data.ToString();

            data.Paint(canvas);

            textBlock.Text += data.ToString();
            
        }
    }
}