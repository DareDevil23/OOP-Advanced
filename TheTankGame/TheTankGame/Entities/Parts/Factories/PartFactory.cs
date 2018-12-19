namespace TheTankGame.Entities.Parts.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using TheTankGame.Entities.Parts.Contracts;
    using Contracts;

    public class PartFactory : IPartFactory
    {
        public IPart CreatePart(string partType, string model, double weight, decimal price, int additionalParameter)
        {
            string partTypeName = partType + "Part";

            var type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(t => t.Name == partTypeName);

            var partInstance = (IPart)Activator.CreateInstance(type, new object[] {model, weight, price, additionalParameter});

            return partInstance;
        }
    }
}
