﻿namespace Travel
{
    using Core;
    using Core.Contracts;
    using Core.Controllers;
    using Core.Controllers.Contracts;
    using Core.IO;
    using Core.IO.Contracts;
    using Entities;
    using Entities.Contracts;

    public static class StartUp
	{
		public static void Main(string[] args)
		{
            IAirport airport = new Airport();
            IAirportController airportController = new AirportController(airport);
            IFlightController flightController = new FlightController(airport);

		    IReader reader = new ConsoleReader();
		    IWriter writer = new ConsoleWriter();
            IEngine engine = new Engine(reader, writer, airportController, flightController);

            engine.Run();
		}
	}
}