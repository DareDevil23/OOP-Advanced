﻿namespace Travel.Core
{
	using System.Linq;
	using Contracts;
	using Controllers.Contracts;
	using IO.Contracts;

	public class Engine : IEngine
	{
	    private const string EndCommand = "END";

		private readonly IReader reader;
		private readonly IWriter writer;

		private readonly IAirportController airportController;
		private readonly IFlightController flightController;

		public Engine(IReader reader, IWriter writer, IAirportController airportController,
			IFlightController flightController)
		{
			this.reader = reader;
			this.writer = writer;
			this.airportController = airportController;
			this.flightController = flightController;
		}

	    public void Run()
	    {
	        string input = string.Empty;

	        while ((input = this.reader.ReadLine()) != EndCommand)
	        {
	            try
	            {
	                string result = this.ProcessCommand(input);
	                writer.WriteLine(result);
	            }
	            catch (System.InvalidOperationException ex)
	            {
	                writer.WriteLine("ERROR: " + ex.Message);
	            }
	        }
        }

	    public string ProcessCommand(string input)
		{
			var tokens = input.Split(' ');

			string command = tokens[0];
			string[] args = tokens.Skip(1).ToArray();

			switch (command)
			{
				case "RegisterPassenger":
				{
					var name = args[0];
					var output = this.airportController.RegisterPassenger(name);
					return output;
				}
				case "RegisterTrip":
				{
					var source = args[0];
					var destination = args[1];
					var planeType = args[2];

					var output = this.airportController.RegisterTrip(source, destination, planeType);
					return output;
				}
				case "RegisterBag":
				{
					var username = args[0];
					var bagItems = args.Skip(1);

					var output = this.airportController.RegisterBag(username, bagItems);
					return output;
				}
				case "CheckIn":
				{
					var username = args[0];
					var tripId = args[1];
					var bagCheckInIndices = args.Skip(2).Select(int.Parse);

					var output = this.airportController.CheckIn(username, tripId, bagCheckInIndices);
					return output;
				}
				case "TakeOff":
				{
					var output = this.flightController.TakeOff();
					return output;
				}
				default:
					throw new System.InvalidOperationException("Invalid command!");
			}
		}
	}
}