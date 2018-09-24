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
        #region Private Fields

        private const int PinBeginDateTime = 91;
        private const int PinEndDateTime = 92;
        private const int PinTag_1 = 1;
        private const int PoutCaclulatedDiff_1 = 101;

        #endregion Private Fields

        #region Public Constructors

        public DiffFromArchive()
        {
        }

        #endregion Public Constructors



        #region Protected Methods

        protected override void ToRuntime()
        {
            PinChanged += DiffFromArchive_PinChanged;
        }

        #endregion Protected Methods

        #region Private Methods

        private void CalculateDiff(int pinBeginDateTime, int pinEndDateTime, int pinTag, int poutCaclulatedDiff)
        {
            if (IsValueExist(pinTag) &&
                IsValueExist(pinBeginDateTime) && 
                IsValueExist(pinEndDateTime) && 
                GetPinDateTime(pinEndDateTime) > GetPinDateTime(pinBeginDateTime))
            {
                DateTime EndTime = GetPinDateTime(pinEndDateTime).ToUniversalTime();
                DateTime StartTime = GetPinDateTime(pinBeginDateTime).ToUniversalTime();

                IEnumerable<object> query = from x in PinByID(pinTag).TreePinHlp.FirstConnectedItem.DataArchiveItem.Read(StartTime, EndTime, false)
                                            where x.Quality.IsGood
                                            select x.Value;

                if (query.Count() >= 2)
                {
                    SetPinValue(poutCaclulatedDiff, (double)query.Last() - (double)query.First());
                }
                else
                {
                    SetPinValue(poutCaclulatedDiff, 0);
                }
            }
        }

        private void DiffFromArchive_PinChanged(int pinID)
        {
            if (_designMode)
            {
                return;
            }

            if (pinID == PinBeginDateTime)
            {
                for (int i = 1; i < 31; i++)
                {
                    CalculateDiff(PinBeginDateTime, PinEndDateTime, i, 100 + 1);
                }
            }
        }

        #endregion Private Methods
    }
}