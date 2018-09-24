using FB.VisualFB;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace MasterScadaBlocks.DiffDateTimePicker
{
    [ComVisible(true),
     Guid("0A5EF31C-8B66-443A-9958-039D3BA2FAC1"),
     DisplayName("DiffDateTimePicker")]
    public partial class DiffDateTimePickerWF : VisualControlBase
    {
        #region Private Fields

        private DiffDateTimePickerWPF _controlWpf;

        #endregion Private Fields

        #region Public Constructors

        public DiffDateTimePickerWF()
        {
            InitializeComponent();
        }

        #endregion Public Constructors



        #region Protected Methods

        protected override void ToRuntime()
        {
            _controlWpf.OnCheckboxChanged(true);
        }

        #endregion Protected Methods

        #region Private Methods

        private void _controlWpf_CheckboxChanged(object sender, ChangedEventArgs e)
        {
            if (FBConnector.DesignMode)
            {
                return;
            }

            FBConnector.SetPinValue(DiffDateTimePicker.PinBeginTimeVFB, e.BeginTime);
            FBConnector.SetPinValue(DiffDateTimePicker.PinEndTimeVFB, e.EndTime);
        }

        private void ControlLoad(object sender, EventArgs e)
        {
            _controlWpf = new DiffDateTimePickerWPF();
            ElementHost _host = new ElementHost { Dock = DockStyle.Fill };
            _host.Child = _controlWpf;
            Controls.Add(_host);

            _controlWpf.CheckboxChanged += _controlWpf_CheckboxChanged;
        }

        #endregion Private Methods
    }
}