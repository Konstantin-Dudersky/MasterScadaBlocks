using FB;
using FB.VisualFB;
using InSAT.Library.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MasterScadaBlocks.Pie
{
    [Serializable,
    ComVisible(true),
    Guid("F8F8A91A-0428-4A56-B3F4-88B52ABA4DB2"),
    CatID(CatIDs.CATID_OTHER),
    DisplayName("Pie chart"),
    ControllerCode(54),
    FBOptions(FBOptions.UseScanByTime),
    VisualControls(typeof(PieWF))]
    public class Pie : VisualFBBase
    {
        private const int Percent1 = 1;
        private const int Percent2 = 2;
        private const int Percent3 = 3;
        private const int Percent4 = 4;
        private const int Percent5 = 5;
        private const int Percent6 = 6;
        private const int Percent7 = 7;
        private const int Percent8 = 8;
        private const int Percent9 = 9;
        public const int Percent1Visual = 101;
        public const int Percent2Visual = 102;
        public const int Percent3Visual = 103;
        public const int Percent4Visual = 104;
        public const int Percent5Visual = 105;
        public const int Percent6Visual = 106;
        public const int Percent7Visual = 107;
        public const int Percent8Visual = 108;
        public const int Percent9Visual = 109;

        protected override void UpdateData()
        {
            VisualPins.SetPinValue(Percent1Visual, GetPinValue<double>(Percent1));
            VisualPins.SetPinValue(Percent2Visual, GetPinValue<double>(Percent2));
            VisualPins.SetPinValue(Percent3Visual, GetPinValue<double>(Percent3));
            VisualPins.SetPinValue(Percent4Visual, GetPinValue<double>(Percent4));
            VisualPins.SetPinValue(Percent5Visual, GetPinValue<double>(Percent5));
            VisualPins.SetPinValue(Percent6Visual, GetPinValue<double>(Percent6));
            VisualPins.SetPinValue(Percent7Visual, GetPinValue<double>(Percent7));
            VisualPins.SetPinValue(Percent8Visual, GetPinValue<double>(Percent8));
            VisualPins.SetPinValue(Percent9Visual, GetPinValue<double>(Percent9));

        }
    }
}