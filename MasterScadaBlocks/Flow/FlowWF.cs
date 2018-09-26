using MasterScadaBlocksCommon;
using FB.VisualFB;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Threading;

namespace MasterScadaBlocks.Flow
{
    [ComVisible(true),
     Guid("2696B53A-4D16-496E-97DD-CCCB05FAFEC5"),
     DisplayName("Flow")]
    public partial class FlowWF : VisualControlBase
    {
        #region Private Fields
       
        private FlowWPF _controlWpf;
        private NodeCollection data = null;
        private DispatcherTimer timer = null;
        private bool updateLocked = false;

        #endregion Private Fields

        #region Public Constructors

        public FlowWF()
        {
            InitializeComponent();
        }

        #endregion Public Constructors



        #region Public Properties

        [DisplayName("Уровень 1"), Description("Название"), Category("_Настройки уровней узлов для отображения")]
        public string Level0 { get; set; } = "0";

        [DisplayName("Уровень 2"), Description("Название"), Category("_Настройки уровней узлов для отображения")]
        public string Level1 { get; set; } = "0";

        [DisplayName("Уровень 3"), Description("Название"), Category("_Настройки уровней узлов для отображения")]
        public string Level2 { get; set; } = "0";

        [DisplayName("Уровень 4"), Description("Название"), Category("_Настройки уровней узлов для отображения")]
        public string Level3 { get; set; } = "0";

        [DisplayName("Уровень 5"), Description("Название"), Category("_Настройки уровней узлов для отображения")]
        public string Level4 { get; set; } = "0";

        [DisplayName("Уровень 6"), Description("Название"), Category("_Настройки уровней узлов для отображения")]
        public string Level5 { get; set; } = "0";

        [DisplayName("Узел 01, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node01Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 01, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node01Name { get; set; } = "Узел 1";

        [DisplayName("Узел 02, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node02Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi1);

        [DisplayName("Узел 02, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node02Name { get; set; } = "Узел 2";

        [DisplayName("Узел 03, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node03Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi2);

        [DisplayName("Узел 03, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node03Name { get; set; } = "Узел 3";

        [DisplayName("Узел 04, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node04Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi3);

        [DisplayName("Узел 04, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node04Name { get; set; } = "Узел 4";

        [DisplayName("Узел 05, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node05Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi4);

        [DisplayName("Узел 05, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node05Name { get; set; } = "Узел 5";

        [DisplayName("Узел 06, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node06Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi5);

        [DisplayName("Узел 06, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node06Name { get; set; } = "Узел 6";

        [DisplayName("Узел 07, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node07Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi6);

        [DisplayName("Узел 07, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node07Name { get; set; } = "Узел 7";

        [DisplayName("Узел 08, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node08Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi7);

        [DisplayName("Узел 08, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node08Name { get; set; } = "Узел 8";

        [DisplayName("Узел 09, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node09Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi8);

        [DisplayName("Узел 09, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node09Name { get; set; } = "Узел 9";

        [DisplayName("Узел 10, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node10Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi9);

        [DisplayName("Узел 10, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node10Name { get; set; } = "Узел 10";

        [DisplayName("Узел 11, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node11Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 11, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node11Name { get; set; } = "Узел 11";

        [DisplayName("Узел 12, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node12Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 12, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node12Name { get; set; } = "Узел 12";

        [DisplayName("Узел 13, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node13Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 13, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node13Name { get; set; } = "Узел 13";

        [DisplayName("Узел 14, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node14Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 14, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node14Name { get; set; } = "Узел 14";

        [DisplayName("Узел 15, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node15Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 15, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node15Name { get; set; } = "Узел 15";

        [DisplayName("Узел 16, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node16Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 16, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node16Name { get; set; } = "Узел 16";

        [DisplayName("Узел 17, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node17Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 17, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node17Name { get; set; } = "Узел 17";

        [DisplayName("Узел 18, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node18Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 18, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node18Name { get; set; } = "Узел 18";

        [DisplayName("Узел 19, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node19Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 19, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node19Name { get; set; } = "Узел 19";

        [DisplayName("Узел 20, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node20Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 20, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node20Name { get; set; } = "Узел 20";

        [DisplayName("Узел 21, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node21Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 21, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node21Name { get; set; } = "Узел 21";

        [DisplayName("Узел 22, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node22Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 22, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node22Name { get; set; } = "Узел 22";

        [DisplayName("Узел 23, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node23Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 23, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node23Name { get; set; } = "Узел 23";

        [DisplayName("Узел 24, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node24Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 24, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node24Name { get; set; } = "Узел 24";

        [DisplayName("Узел 25, цвет"), Description("Цвет"), Category("_Настройки узлов")]
        public Color Node25Color { get; set; } = (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0);

        [DisplayName("Узел 25, название"), Description("Название"), Category("_Настройки узлов")]
        public string Node25Name { get; set; } = "Узел 25";

        [DisplayName("Поток 01, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink01Loc { get; set; } = "0 0";

        [DisplayName("Поток 02, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink02Loc { get; set; } = "0 0";

        [DisplayName("Поток 03, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink03Loc { get; set; } = "0 0";

        [DisplayName("Поток 04, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink04Loc { get; set; } = "0 0";

        [DisplayName("Поток 05, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink05Loc { get; set; } = "0 0";

        [DisplayName("Поток 06, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink06Loc { get; set; } = "0 0";

        [DisplayName("Поток 07, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink07Loc { get; set; } = "0 0";

        [DisplayName("Поток 08, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink08Loc { get; set; } = "0 0";

        [DisplayName("Поток 09, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink09Loc { get; set; } = "0 0";

        [DisplayName("Поток 10, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink10Loc { get; set; } = "0 0";

        [DisplayName("Поток 11, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink11Loc { get; set; } = "0 0";

        [DisplayName("Поток 12, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink12Loc { get; set; } = "0 0";

        [DisplayName("Поток 13, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink13Loc { get; set; } = "0 0";

        [DisplayName("Поток 14, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink14Loc { get; set; } = "0 0";

        [DisplayName("Поток 15, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink15Loc { get; set; } = "0 0";

        [DisplayName("Поток 16, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink16Loc { get; set; } = "0 0";

        [DisplayName("Поток 17, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink17Loc { get; set; } = "0 0";

        [DisplayName("Поток 18, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink18Loc { get; set; } = "0 0";

        [DisplayName("Поток 19, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink19Loc { get; set; } = "0 0";

        [DisplayName("Поток 20, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink20Loc { get; set; } = "0 0";

        [DisplayName("Поток 21, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink21Loc { get; set; } = "0 0";

        [DisplayName("Поток 22, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink22Loc { get; set; } = "0 0";

        [DisplayName("Поток 23, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink23Loc { get; set; } = "0 0";

        [DisplayName("Поток 24, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink24Loc { get; set; } = "0 0";

        [DisplayName("Поток 25, от и до"), Description("Название"), Category("_Настройки потоков")]
        public string NodeLink25Loc { get; set; } = "0 0";

        #endregion Public Properties



        #region Protected Methods

        protected override void ToRuntime()
        {
            base.ToRuntime();
            FBConnector_PinChanged(0);
        }

        #endregion Protected Methods

        #region Private Methods

        private void ControlLoad(object sender, EventArgs e)
        {
            _controlWpf = new FlowWPF();
            ElementHost _host = new ElementHost { Dock = DockStyle.Fill };
            _host.Child = _controlWpf;
            Controls.Add(_host);

            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3),
            };
            timer.Tick += Timer_Tick;

            FBConnector.PinChanged += FBConnector_PinChanged;

            //data = new NodeCollection();

            //data.AddNode(0, "Net supply", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi0));
            //data.AddNode(1, "PV", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi1));
            //data.AddNode(2, "Factory", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi2));
            //data.AddNode(3, "Utilities", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi3));
            //data.AddNode(4, "Admin Building", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi4));
            //data.AddNode(5, "Production", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi5));
            //data.AddNode(6, "Packaging", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi6));
            //data.AddNode(7, "Compressor", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi7));
            //data.AddNode(8, "HVAC", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi8));
            //data.AddNode(9, "Boiler", (Color)ColorConverter.ConvertFromString(CommonClass.ColorPowerBi9));

            //data.AddNodeLink(0, 4, 3185);
            //data.AddNodeLink(0, 2, 57426);
            //data.AddNodeLink(0, 3, 4259);
            //data.AddNodeLink(1, 2, 2544);
            //data.AddNodeLink(2, 5, 42322);
            //data.AddNodeLink(2, 6, 15104);
            //data.AddNodeLink(3, 7, 704);
            //data.AddNodeLink(3, 8, 2316);
            //data.AddNodeLink(3, 9, 1239);

            //data.AddLevels(0, "1 2");
            //data.AddLevels(1, "3 4");
            //data.AddLevels(2, "5 6 7 8 9 10");

            //data.Paint(_controlWpf.canvas);
        }

        private void FBConnector_PinChanged(int pinID)
        {
            if (FBConnector.DesignMode)
            {
                return;
            }

            if (updateLocked)
            {
                return;
            }
            else
            {
                updateLocked = true;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            updateLocked = false;

            data = new NodeCollection();

            data.AddNode(0, Node01Name, Node01Color);
            data.AddNode(1, Node02Name, Node02Color);
            data.AddNode(2, Node03Name, Node03Color);
            data.AddNode(3, Node04Name, Node04Color);
            data.AddNode(4, Node05Name, Node05Color);
            data.AddNode(5, Node06Name, Node06Color);
            data.AddNode(6, Node07Name, Node07Color);
            data.AddNode(7, Node08Name, Node08Color);
            data.AddNode(8, Node09Name, Node09Color);
            data.AddNode(9, Node10Name, Node10Color);
            data.AddNode(10, Node11Name, Node11Color);
            data.AddNode(11, Node12Name, Node12Color);
            data.AddNode(12, Node13Name, Node13Color);
            data.AddNode(13, Node14Name, Node14Color);
            data.AddNode(14, Node15Name, Node15Color);
            data.AddNode(15, Node16Name, Node16Color);
            data.AddNode(16, Node17Name, Node17Color);
            data.AddNode(17, Node18Name, Node18Color);
            data.AddNode(18, Node19Name, Node19Color);
            data.AddNode(19, Node20Name, Node20Color);
            data.AddNode(20, Node21Name, Node21Color);
            data.AddNode(21, Node22Name, Node22Color);
            data.AddNode(22, Node23Name, Node23Color);
            data.AddNode(23, Node24Name, Node24Color);
            data.AddNode(24, Node25Name, Node25Color);

            data.AddNodeLink(NodeLink01Loc, FBConnector.GetPinDouble(101));
            data.AddNodeLink(NodeLink02Loc, FBConnector.GetPinDouble(102));
            data.AddNodeLink(NodeLink03Loc, FBConnector.GetPinDouble(103));
            data.AddNodeLink(NodeLink04Loc, FBConnector.GetPinDouble(104));
            data.AddNodeLink(NodeLink05Loc, FBConnector.GetPinDouble(105));
            data.AddNodeLink(NodeLink06Loc, FBConnector.GetPinDouble(106));
            data.AddNodeLink(NodeLink07Loc, FBConnector.GetPinDouble(107));
            data.AddNodeLink(NodeLink08Loc, FBConnector.GetPinDouble(108));
            data.AddNodeLink(NodeLink09Loc, FBConnector.GetPinDouble(109));
            data.AddNodeLink(NodeLink10Loc, FBConnector.GetPinDouble(110));
            data.AddNodeLink(NodeLink11Loc, FBConnector.GetPinDouble(111));
            data.AddNodeLink(NodeLink12Loc, FBConnector.GetPinDouble(112));
            data.AddNodeLink(NodeLink13Loc, FBConnector.GetPinDouble(113));
            data.AddNodeLink(NodeLink14Loc, FBConnector.GetPinDouble(114));
            data.AddNodeLink(NodeLink15Loc, FBConnector.GetPinDouble(115));
            data.AddNodeLink(NodeLink16Loc, FBConnector.GetPinDouble(116));
            data.AddNodeLink(NodeLink17Loc, FBConnector.GetPinDouble(117));
            data.AddNodeLink(NodeLink18Loc, FBConnector.GetPinDouble(118));
            data.AddNodeLink(NodeLink19Loc, FBConnector.GetPinDouble(119));
            data.AddNodeLink(NodeLink20Loc, FBConnector.GetPinDouble(120));
            data.AddNodeLink(NodeLink21Loc, FBConnector.GetPinDouble(121));
            data.AddNodeLink(NodeLink22Loc, FBConnector.GetPinDouble(122));
            data.AddNodeLink(NodeLink23Loc, FBConnector.GetPinDouble(123));
            data.AddNodeLink(NodeLink24Loc, FBConnector.GetPinDouble(124));
            data.AddNodeLink(NodeLink25Loc, FBConnector.GetPinDouble(125));

            data.AddLevels(0, Level0);
            data.AddLevels(1, Level1);
            data.AddLevels(2, Level2);
            data.AddLevels(3, Level3);
            data.AddLevels(4, Level4);
            data.AddLevels(5, Level5);

            data.Paint(_controlWpf.canvas);
        }

        #endregion Private Methods
    }
}