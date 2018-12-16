namespace FestivalManager.Entities.Sets
{
	using System;

	public class Medium : Set
	{
	    private const int MediumSetMaxDurationMinutes = 40;

        public Medium(string name)
			: base(name, new TimeSpan(0, MediumSetMaxDurationMinutes, 0))
		{

		}
	}
}