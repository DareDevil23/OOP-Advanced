namespace FestivalManager.Entities.Factories
{
	using Contracts;
	using Entities.Contracts;
	using System;
	using System.Linq;
	using System.Reflection;

    public class SetFactory : ISetFactory
	{
		public ISet CreateSet(string name, string typeName)
		{
		    Type type = Assembly.GetCallingAssembly().GetTypes().SingleOrDefault(t => t.Name == typeName);

		    var result = (ISet)Activator.CreateInstance(type, name);

		    return result;
		}
	}

}
