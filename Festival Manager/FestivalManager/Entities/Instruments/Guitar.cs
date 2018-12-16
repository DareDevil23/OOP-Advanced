namespace FestivalManager.Entities.Instruments
{
    public class Guitar : Instrument
    {

        private const int GuitarRepairAmountConst = 60;

        protected override int RepairAmount => GuitarRepairAmountConst;
    }
}
