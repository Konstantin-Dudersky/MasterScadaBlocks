using FB.VisualFB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;

namespace MasterScadaBlocks.Pie
{
    [ComVisible(true),
     Guid("1E4B9200-2739-4F28-9BA0-D830FB7A41EA"),
     DisplayName("Pie Chart")]
    public partial class PieWF : VisualControlBase
    {
        private PieWPF _controlWpf;
        private List<Color> colors;

        #region Properties
        private Color color1 = Color.FromArgb(255, 1, 184, 170);
        [DisplayName("Цвет"), Description("Цвет"), Category("Категория 1")]
        public Color Color1 { get => color1; set => color1 = value; }

        private string name1 = "Значение 1";
        [DisplayName("Название"), Description("Название"), Category("Категория 1")]
        public string Name1 { get => name1; set => name1 = value; }


        private Color color2 = Color.FromArgb(255, 55, 70, 73);
        [DisplayName("Цвет 2"), Description("Описание"), Category("Цвета")]
        public Color Color2 { get => color2; set => color2 = value; }

        private Color color3 = Color.FromArgb(255, 253, 98, 94);
        [DisplayName("Цвет 3"), Description("Описание"), Category("Цвета")]
        public Color Color3 { get => color3; set => color3 = value; }

        private Color color4 = Color.FromArgb(255, 242, 200, 15);
        [DisplayName("Цвет 4"), Description("Описание"), Category("Цвета")]
        public Color Color4 { get => color4; set => color4 = value; }

        private Color color5 = Color.FromArgb(255, 95, 107, 109);
        [DisplayName("Цвет 5"), Description("Описание"), Category("Цвета")]
        public Color Color5 { get => color5; set => color5 = value; }

        private Color color6 = Color.FromArgb(255, 138, 212, 235);
        [DisplayName("Цвет 6"), Description("Описание"), Category("Цвета")]
        public Color Color6 { get => color6; set => color6 = value; }

        private Color color7 = Color.FromArgb(255, 254, 150, 102);
        [DisplayName("Цвет 7"), Description("Описание"), Category("Цвета")]
        public Color Color7 { get => color7; set => color7 = value; }

        private Color color8 = Color.FromArgb(255, 166, 105, 153);
        [DisplayName("Цвет 8"), Description("Описание"), Category("Цвета")]
        public Color Color8 { get => color8; set => color8 = value; }

        private Color color9 = Color.FromArgb(255, 53, 153, 184);
        [DisplayName("Цвет 9"), Description("Описание"), Category("Цвета")]
        public Color Color9 { get => color9; set => color9 = value; }

        private Color color10 = Color.FromArgb(255, 223, 191, 191);
        [DisplayName("Цвет 10"), Description("Описание"), Category("Цвета")]
        public Color Color10 { get => color10; set => color10 = value; }
        #endregion

        private DiagItemCollection diagram = new DiagItemCollection();

        public PieWF()
        {
            diagram.Add(Name1, Color1);
            diagram.Add("1", Color2);
            diagram.Add("2", Color3);
            diagram.Add("3", Color4);
            diagram.Add("4", Color5);
            diagram.Add("5", Color6);
            diagram.Add("6", Color7);
            diagram.Add("7", Color8);
            diagram.Add("8", Color9);
            diagram.Add("9", Color10);

            InitializeComponent();
            FBConnector.PinChanged += FBConnector_PinChanged;

            colors = new List<Color> { color1, color2, color3, color4, color5, color6, color7, color8, color9, color10 };
        }

        protected override void ToRuntime()
        {
            base.ToRuntime();
            FBConnector_PinChanged(0);
        }

        private void ControlLoad(object sender, EventArgs e)
        {
            _controlWpf = new PieWPF(400, 200);
            ElementHost _host = new ElementHost { Dock = DockStyle.Fill };
            _host.Child = _controlWpf;
            Controls.Add(_host);

            _controlWpf.Draw(120, 120, 100, new double[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, colors, diagram);
        }

        private void FBConnector_PinChanged(int pinID)
        {
            if (FBConnector.DesignMode)
            {
                return;
            }

            double[] arr = {
                FBConnector.GetPinDouble(Pie.Percent1Visual),
                FBConnector.GetPinDouble(Pie.Percent2Visual),
                FBConnector.GetPinDouble(Pie.Percent3Visual),
                FBConnector.GetPinDouble(Pie.Percent4Visual),
                FBConnector.GetPinDouble(Pie.Percent5Visual),
                FBConnector.GetPinDouble(Pie.Percent6Visual),
                FBConnector.GetPinDouble(Pie.Percent7Visual),
                FBConnector.GetPinDouble(Pie.Percent8Visual),
                FBConnector.GetPinDouble(Pie.Percent9Visual),
            };

            diagram[0].Value = FBConnector.GetPinDouble(Pie.Percent1Visual);
            diagram[1].Value = FBConnector.GetPinDouble(Pie.Percent2Visual);
            diagram[2].Value = FBConnector.GetPinDouble(Pie.Percent3Visual);
            diagram[3].Value = FBConnector.GetPinDouble(Pie.Percent4Visual);
            diagram[4].Value = FBConnector.GetPinDouble(Pie.Percent5Visual);
            diagram[5].Value = FBConnector.GetPinDouble(Pie.Percent6Visual);
            diagram[6].Value = FBConnector.GetPinDouble(Pie.Percent7Visual);
            diagram[7].Value = FBConnector.GetPinDouble(Pie.Percent8Visual);
            diagram[8].Value = FBConnector.GetPinDouble(Pie.Percent9Visual);
            diagram.SetPercent();

            _controlWpf.Draw(120, 120, 100, arr, colors, diagram);
        }
    }
}