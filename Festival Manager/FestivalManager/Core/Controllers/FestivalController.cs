using System.Text;

namespace FestivalManager.Core.Controllers
{
	using System;
	using System.Linq;
	using Contracts;
	using Entities.Contracts;
	using Entities;
	using Entities.Factories;
	using FestivalManager.Entities.Factories.Contracts;

    public class FestivalController : IFestivalController
	{
		private const string TimeFormat = "mm\\:ss";

		private readonly IStage stage;
	    private readonly IInstrumentFactory instrumentFactory;
	    private readonly ISetFactory setFactory;

		public FestivalController(IStage stage)
		{
			this.stage = stage;
            this.instrumentFactory = new InstrumentFactory();
            this.setFactory = new SetFactory();
		}

	    public string RegisterSet(string[] args)
	    {
	        string name = args[0];
	        string type = args[1];

	        ISet set = this.setFactory.CreateSet(name, type);

            this.stage.AddSet(set);

	        return $"Registered {type} set";
	    } // completed

	    public string SignUpPerformer(string[] args)
	    {
	        var name = args[0];
	        var age = int.Parse(args[1]);

	        var performer = new Performer(name, age);

            var instrumentsTokens = args.Skip(2).ToArray();

	        IInstrument[] instruments = instrumentsTokens
	            .Select(i => this.instrumentFactory.CreateInstrument(i))
	            .ToArray();

	        foreach (var instrument in instruments)
	        {
	            performer.AddInstrument(instrument);
	        }

	        this.stage.AddPerformer(performer);

	        return $"Registered performer {name}";
	    } //completed

	    public string RegisterSong(string[] args)
	    {
	        string songName = args[0];

	        string timeAsString = args[1];
	        int[] timeTokens = timeAsString.Trim().Split(":").Select(int.Parse).ToArray();
	        int minutes = timeTokens[0];
	        int seconds = timeTokens[1];

            TimeSpan duration = new TimeSpan(0, minutes, seconds);

	        ISong song = new Song(songName, duration);

            string time = FormatTimeSpanToString(duration);

            this.stage.AddSong(song);

	        return $"Registered song {songName} ({time})";
	    } //Completed 

	    public string AddSongToSet(string[] args)
	    {
	        var songName = args[0];
	        var setName = args[1];

	        if (!this.stage.HasSet(setName))
	        {
	            throw new InvalidOperationException("Invalid set provided");
	        }

	        if (!this.stage.HasSong(songName))
	        {
	            throw new InvalidOperationException("Invalid song provided");
	        }

	        var set = this.stage.GetSet(setName);
	        var song = this.stage.GetSong(songName);

	        set.AddSong(song);

            var songDuration = this.FormatTimeSpanToString(song.Duration);

	        return $"Added {songName} ({songDuration}) to {setName}";
	    } // completed

	    public string AddPerformerToSet(string[] args)
	    {
	        var performerName = args[0];
	        var setName = args[1];

	        if (!this.stage.HasPerformer(performerName))
	        {
	            throw new InvalidOperationException("Invalid performer provided");
	        }

	        if (!this.stage.HasSet(setName))
	        {
	            throw new InvalidOperationException("Invalid set provided");
	        }


	        IPerformer performer = this.stage.GetPerformer(performerName);
	        ISet set = this.stage.GetSet(setName);

	        set.AddPerformer(performer);

	        return $"Added {performerName} to {setName}";
	    } //completed

	    public string RepairInstruments(string[] args)
	    {
	        IInstrument[] instrumentsToRepair = this.stage
	            .Performers
	            .SelectMany(p => p.Instruments)
	            .Where(i => i.Wear < 100)
	            .ToArray();

	        foreach (IInstrument instrument in instrumentsToRepair)
	        {
	            instrument.Repair();
	        }

	        return $"Repaired {instrumentsToRepair.Length} instruments";
	    } //Completed

        public string ProduceReport()
	    {
	        StringBuilder sb = new StringBuilder();

	        var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));  //

	        sb.AppendLine($"Festival length: {this.FormatTimeSpanToString(totalFestivalLength)}"); 

	        foreach (var set in this.stage.Sets)
	        {
	            sb.AppendLine($"--{set.Name} ({this.FormatTimeSpanToString(set.ActualDuration)}):");

	            var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
	            foreach (var performer in performersOrderedDescendingByAge)
	            {
	                var instruments = string.Join(", ", performer.Instruments
	                    .OrderByDescending(i => i.Wear).Select(i =>  $"{i.ToString()}"));

	                sb.AppendLine($"---{performer.Name} ({instruments})");
	            } 

	            if (!set.Songs.Any())
	                sb.AppendLine("--No songs played");
	            else
	            {
	                sb.AppendLine("--Songs played:");
	                foreach (var song in set.Songs)
	                {
	                    sb.AppendLine($"----{song.Name} ({song.Duration.ToString(TimeFormat)})");
	                }
	            }
	        }

	        return sb.ToString().Trim();
        }  //Completed

	    private string FormatTimeSpanToString(TimeSpan timeSpan)
	    {
	        int minutes = timeSpan.Hours * 60 + timeSpan.Minutes;
	        int seconds = timeSpan.Seconds;

	        string result = $"{minutes:D2}:{seconds:D2}";
	        return result;
	    }
    }
}