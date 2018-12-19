namespace TheTankGame.Entities.Vehicles.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using TheTankGame.Entities.Vehicles.Contracts;
    using Contracts;
    using Miscellaneous;

    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle CreateVehicle(
            string vehicleType, string model, double weight, decimal price, int attack, int defense,int hitPoints)
        {
            var type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(t => t.Name == vehicleType);

            object[] args = {model, weight, price, attack, defense, hitPoints, new VehicleAssembler()};

            var vehicleInstance = (IVehicle) Activator.CreateInstance(type, args);

            return vehicleInstance;
        }
    }
}
