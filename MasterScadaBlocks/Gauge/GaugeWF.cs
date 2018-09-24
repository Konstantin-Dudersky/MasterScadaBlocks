using FB.VisualFB;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace MasterScadaBlocks.Gauge
{
    [ComVisible(true),
     Guid("44741728-294C-4C67-BCF5-C760A2CE8AB0"),
     DisplayName("Gauge")]
    public partial class GaugeWF : VisualControlBase
    {
        private GaugeWPF _controlWpf;
        private GaugeData data;

        private double h = 80.0;
        private string header = "Заголовок";
        private double hh = 90.0;
        private double l = 20.0;
        private double ll = 10.0;
        private double max = 100.0;
        private double min = 0.0;

        public GaugeWF()
        {
            InitializeComponent();
            data = new GaugeData();
            SetDataProp();

            FBConnector.PinChanged += FBConnector_PinChanged;
        }

        [DisplayName("Верхний предупредительный"), Description("Верхн. предупредительный"), Category("_Настройки")]
        public double H { get => h; set => h = value; }

        [DisplayName("Заголовок"), Description("Заголовок"), Category("_Настройки")]
        public string Header {
            get => header;
            set => header = value;
        }

        [DisplayName("Верхний аварийный"), Description("Верхн. аварийный"), Category("_Настройки")]
        public double HH { get => hh; set => hh = value; }

        [DisplayName("Нижний предупредительный"), Description("Нижний предупредительный"), Category("_Настройки")]
        public double L { get => l; set => l = value; }

        [DisplayName("Нижний аварийный"), Description("Нижний аварийный"), Category("_Настройки")]
        public double LL { get => ll; set => ll = value; }

        [DisplayName("Максимум"), Description("Максимум"), Category("_Настройки")]
        public double Max { get => max; set => max = value; }

        [DisplayName("Минимум"), Description("Минимум"), Category("_Настройки")]
        public double Min { get => min; set => min = value; }

        protected override void ToRuntime()
        {
            base.ToRuntime();
            SetDataProp();
            FBConnector_PinChanged(0);
        }

        private void ControlLoad(object sender, EventArgs e)
        {
            _controlWpf = new GaugeWPF();
            ElementHost _host = new ElementHost { Dock = DockStyle.Fill };
            _host.Child = _controlWpf;
            Controls.Add(_host);

            _controlWpf.Paint(data);
        }

        private void FBConnector_PinChanged(int pinID)
        {
            if (FBConnector.DesignMode)
            {
                return;
            }

            data.Value = FBConnector.GetPinDouble(Gauge.PinValueVFB);
            _controlWpf.Paint(data);
        }

        private void SetDataProp()
        {
            data.Min = Min;
            data.Max = Max;
            data.H = H;
            data.HH = HH;
            data.L = L;
            data.LL = LL;
            data.Value = 20;
            data.Header = Header;
        }
    }
}