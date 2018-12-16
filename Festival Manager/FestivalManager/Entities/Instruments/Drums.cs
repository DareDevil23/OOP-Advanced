namespace FestivalManager.Entities.Instruments
{
	public class Drums : Instrument
	{
		private const int DrumsRepairAmount = 20;

	    protected override int RepairAmount => DrumsRepairAmount;
	}
}
