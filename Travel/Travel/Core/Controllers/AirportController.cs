using Travel.Entities.Airplanes.Contracts;

namespace Travel.Core.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Contracts;
	using Entities;
	using Entities.Contracts;
	using Entities.Factories;
	using Entities.Factories.Contracts;

	public class AirportController : IAirportController
	{
		private const int BagValueConfiscationThreshold = 3000;

		private readonly IAirport airport;
	    private readonly IAirplaneFactory airplaneFactory;
		private readonly IItemFactory itemFactory;

		public AirportController(IAirport airport)
		{
			this.airport = airport;
			this.airplaneFactory = new AirplaneFactory();
			this.itemFactory = new ItemFactory();
		}

        public string RegisterPassenger(string username)
		{
			if (this.airport.GetPassenger(username) != null)
			{
				throw new InvalidOperationException($"Passenger {username} already registered!");
			}

			IPassenger passenger = new Passenger(username);

			this.airport.AddPassenger(passenger);

			return $"Registered {passenger.Username}";
		} 

		public string RegisterTrip(string source, string destination, string planeType)
		{
			IAirplane airplane = this.airplaneFactory.CreateAirplane(planeType);

			ITrip trip = new Trip(source, destination, airplane);

			this.airport.AddTrip(trip); 

			return $"Registered trip {trip.Id}";
		} 

	    public string RegisterBag(string username, IEnumerable<string> bagItems)
	    {
	        var passenger = this.airport.GetPassenger(username);

	        var items = bagItems.Select(i => this.itemFactory.CreateItem(i)).ToArray();

	        var bag = new Bag(passenger, items);

	        passenger.Bags.Add(bag);

	        return $"Registered bag with {string.Join(", ", bagItems)} for {username}";
	    }

        public string CheckIn(string username, string tripId, IEnumerable<int> bagIndices)
		{
            var passenger = this.airport.GetPassenger(username);
		    ITrip trip = this.airport.GetTrip(tripId);

		    var checkedIn = this.airport.Trips.Any(t => t.Airplane.Passengers.Contains(passenger));
            if (checkedIn)
            {
                throw new InvalidOperationException($"{passenger.Username} is already checked in!");
            }

            var confiscatedBags = this.CheckInBags(passenger, bagIndices);
            trip.Airplane.AddPassenger(passenger);

		    var bagCount = bagIndices.Count();

            return
                $"Checked in {passenger.Username} with {bagCount - confiscatedBags}/{bagCount} checked in bags";
        }

		private int CheckInBags(IPassenger passenger, IEnumerable<int> bagsToCheckIn)
		{
			var bags = passenger.Bags;

			var confiscatedBagsCount = 0;
			foreach (int bagIndex in bagsToCheckIn)
			{
				var currentBag = bags[bagIndex];
				bags.RemoveAt(bagIndex);

				if (this.ShouldConfiscate(currentBag))
				{
					airport.AddConfiscatedBag(currentBag);
					confiscatedBagsCount++;
				}
				else
				{
					this.airport.AddCheckedBag(currentBag);
				}
			}

			return confiscatedBagsCount;
		}

		private  bool ShouldConfiscate(IBag bag)
		{
		    var bagItems = bag.Items.ToList();

		    int luggageValue = bagItems.Sum(t => t.Value);

		    bool shouldConfiscate = luggageValue > BagValueConfiscationThreshold;

			return shouldConfiscate;
		}

	}
}