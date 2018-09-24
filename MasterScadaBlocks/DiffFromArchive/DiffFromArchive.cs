using FB;
using InSAT.Library.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace MasterScadaBlocks.DiffFromArchive
{
    [Serializable,
    ComVisible(true),
    Guid("469E2E53-A5DB-4B9A-A7B0-FC5F176BCFEF"),
    CatID(CatIDs.CATID_OTHER),
    DisplayName("Разница из архива")]
    public class DiffFromArchive : StaticFBBase
    {
        private const int PinBeginDateTime = 3;
        private const int PinCalculate = 1;
        private const int PinEndDateTime = 4;
        private const int PinTag = 2;
        private const int PoutCaclulatedDiff = 5;

        protected override void UpdateData()
        {
            bool? calculateOld = false;

            if (
                //GetPinBool(PinCalculate) == true &&
                //calculateOld == false &&
                IsValueExist(PinBeginDateTime) &&
                IsValueExist(PinEndDateTime) &&
                GetPinDateTime(PinEndDateTime) > GetPinDateTime(PinBeginDateTime))
            {
                MasterSCADA.Hlp.ITreePinHlp elem = PinByID(PinTag).TreePinHlp.FirstConnectedItem;
                MasterSCADA.Hlp.Archive.PinDataArchiveHlp k = elem.DataArchiveItem;

                DateTime EndTime = GetPinDateTime(PinEndDateTime).ToUniversalTime();
                DateTime StartTime = GetPinDateTime(PinBeginDateTime).ToUniversalTime();
                MasterSCADA.Hlp.PinValue[] mas = k.Read(StartTime, EndTime, false);

                IEnumerable<object> query = from x in mas
                                            where x.Quality.IsGood
                                            select x.Value;

                SetPinValue(PoutCaclulatedDiff, (double)query.Last() - (double)query.First());
            }

            calculateOld = GetPinBool(PinCalculate);
        }
    }
}