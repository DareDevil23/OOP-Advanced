using System.Linq;

namespace FestivalManager.Entities
{
	using System.Collections.Generic;
	using Contracts;

	public class Stage : IStage
	{
		private readonly List<ISet> sets;
		private readonly List<ISong> songs;
		private readonly List<IPerformer> performers;

	    public Stage()
	    {
	        this.sets = new List<ISet>();
            this.songs = new List<ISong>();
            this.performers = new List<IPerformer>();
	    }

	    public IReadOnlyCollection<ISet> Sets => this.sets.AsReadOnly();

	    public IReadOnlyCollection<ISong> Songs => this.songs.AsReadOnly();

	    public IReadOnlyCollection<IPerformer> Performers => this.performers.AsReadOnly();

	    public IPerformer GetPerformer(string name)
	    {
	        IPerformer performer = this.performers.FirstOrDefault(p => p.Name == name);

	        return performer;
	    }

	    public ISong GetSong(string name)
	    {
	        ISong song = this.songs.FirstOrDefault(s => s.Name == name);

	        return song;
	    }

	    public ISet GetSet(string name)
	    {
	        ISet set = this.sets.FirstOrDefault(s => s.Name == name);

	        return set;
	    }

	    public void AddPerformer(IPerformer performer)
	    {
	        this.performers.Add(performer);
	    }

	    public void AddSong(ISong song)
	    {
	        this.songs.Add(song);
	    }

	    public void AddSet(ISet performer)
	    {
	       this.sets.Add(performer);
	    }

	    public bool HasPerformer(string name)
	    {
	        bool hasPerformer = this.performers.Any(p => p.Name == name);

	        return hasPerformer;
	    }

	    public bool HasSong(string name)
	    {
	        bool hasSong = this.songs.Any(s => s.Name == name);

	        return hasSong;
	    }

	    public bool HasSet(string name)
	    {
	        bool hasSet = this.sets.Any(s => s.Name == name);

	        return hasSet;
	    }
	}
}
