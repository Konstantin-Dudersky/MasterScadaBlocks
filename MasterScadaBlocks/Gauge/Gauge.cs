using FB;
using FB.VisualFB;
using InSAT.Library.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MasterScadaBlocks.Gauge
{
    [Serializable,
    ComVisible(true),
    Guid("653CFB68-49A1-46C1-ABC0-6AD2EB92D108"),
    CatID(CatIDs.CATID_OTHER),
    DisplayName("Gauge"),
    FBOptions(FBOptions.UseScanByTime),
    VisualControls(typeof(GaugeWF))]
    public class Gauge : VisualFBBase
    {
        public const int PinValueVFB = 101;
        private const int PinValue = 1;

        protected override void UpdateData()
        {
            VisualPins.SetPinValue(PinValueVFB, GetPinDouble(PinValue));
        }
    }
}