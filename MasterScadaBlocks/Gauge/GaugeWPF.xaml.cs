using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace MasterScadaBlocks.Gauge
{
    /// <summary>
    /// Interaction logic for GaugeWPF.xaml
    /// </summary>
    public partial class GaugeWPF : UserControl
    {
        public GaugeWPF()
        {
            InitializeComponent();
        }

        public void Paint(GaugeData data)
        {
            data.Paint(canvas);
        }
    }
}
