using FB.VisualFB;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ScadaFb
{
    [ComVisible(true),
     Guid("1E4B9200-2739-4F28-9BA0-D830FB7A41EA"),
     DisplayName("Тест")]
    public partial class TestVisual : VisualControlBase
    {
        private UserControl1 _controlWpf;
        private string[] colors = null;

        public TestVisual()
        {
            InitializeComponent();
        }

        private void ControlLoad(object sender, EventArgs e)
        {
            _controlWpf = new UserControl1();
            ElementHost _host = new ElementHost { Dock = DockStyle.Fill };
            _host.Child = _controlWpf;
            Controls.Add(_host);
            FBConnector.PinChanged += FBConnector_PinChanged;
            VisibleChanged += TestVisual_VisibleChanged;
        }

        private void TestVisual_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                MessageBox.Show("Visible");
                FBConnector_PinChanged(0);
            }
        }

        private void FBConnector_PinChanged(int pinID)
        {
            if (FBConnector.DesignMode)
            {
                return;
            }

            double[] arr = {
                FBConnector.GetPinDouble(TestFB.Percent1Visual),
                FBConnector.GetPinDouble(TestFB.Percent2Visual),
                FBConnector.GetPinDouble(TestFB.Percent3Visual),
                FBConnector.GetPinDouble(TestFB.Percent4Visual),
                FBConnector.GetPinDouble(TestFB.Percent5Visual),
                FBConnector.GetPinDouble(TestFB.Percent6Visual),
                FBConnector.GetPinDouble(TestFB.Percent7Visual),
                FBConnector.GetPinDouble(TestFB.Percent8Visual),
                FBConnector.GetPinDouble(TestFB.Percent9Visual),
            };

            if (colors == null)
            {
                colors = new string[]
                {
                FBConnector.GetPinString(TestFB.Percent1ColorVisual),
                FBConnector.GetPinString(TestFB.Percent2ColorVisual),
                FBConnector.GetPinString(TestFB.Percent3ColorVisual),
                FBConnector.GetPinString(TestFB.Percent4ColorVisual),
                FBConnector.GetPinString(TestFB.Percent5ColorVisual),
                FBConnector.GetPinString(TestFB.Percent6ColorVisual),
                FBConnector.GetPinString(TestFB.Percent7ColorVisual),
                FBConnector.GetPinString(TestFB.Percent8ColorVisual),
                FBConnector.GetPinString(TestFB.Percent9ColorVisual),
                };
            }

            _controlWpf.Draw(100, 100, 100, arr, colors);
        }

    }
}
