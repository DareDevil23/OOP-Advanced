namespace FestivalManager.Entities.Factories
{
	using Contracts;
	using Entities.Contracts;
	using System;
	using System.Linq;
	using System.Reflection;

    public class InstrumentFactory : IInstrumentFactory
	{
		public IInstrument CreateInstrument(string typeName)
		{
		    Type type = Assembly.GetCallingAssembly().GetTypes().SingleOrDefault(t => t.Name == typeName);

		    var result = (IInstrument)Activator.CreateInstance(type);

		    return result;
		}
	}
}