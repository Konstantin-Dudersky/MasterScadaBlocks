using FB;
using FB.VisualFB;
using InSAT.Library.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MasterScadaBlocks.DiffDateTimePicker
{
    [Serializable,
    ComVisible(true),
    Guid("579CD28C-D89D-450B-AA40-8889CA5588BE"),
    CatID(CatIDs.CATID_OTHER),
    DisplayName("DiffDateTimePicker"),
    FBOptions(FBOptions.UseScanByTime),
    VisualControls(typeof(DiffDateTimePickerWF))]
    public class DiffDateTimePicker : VisualFBBase
    {
        public const int PinBeginTimeVFB = 101;
        public const int PinEndTimeVFB = 102;
        private const int PoutBeginTime = 1;
        private const int PoutEndTime = 2;

        public DiffDateTimePicker()
        {
            VisualPins.PinChanged += VisualPins_PinChanged;
        }

        protected override void ToRuntime()
        {
            SetPinValue(PoutBeginTime, DateTime.Now.AddDays(-1));
            SetPinValue(PoutEndTime, DateTime.Now);
        }

        private void VisualPins_PinChanged(int pinID)
        {
            SetPinValue(PoutBeginTime, VisualPins.GetPinValue<DateTime>(PinBeginTimeVFB));
            SetPinValue(PoutEndTime, VisualPins.GetPinValue<DateTime>(PinEndTimeVFB));
        }
    }
}