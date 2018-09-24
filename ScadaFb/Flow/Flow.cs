using FB;
using FB.VisualFB;
using InSAT.Library.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MasterScadaBlocks.Flow
{
    [Serializable,
    ComVisible(true),
    Guid("E14E3E64-5063-46F1-9834-06933DBACF4A"),
    CatID(CatIDs.CATID_OTHER),
    DisplayName("Flow"),
    FBOptions(FBOptions.UseScanByTime),
    VisualControls(typeof(FlowWF))]
    public class Flow : VisualFBBase
    {
        protected override void UpdateData()
        {
            for (int i = 1; i < 26; i++)
            {
                VisualPins.SetValue<double>(i + 100, GetPinDouble(i));
            }
        }
    }
}