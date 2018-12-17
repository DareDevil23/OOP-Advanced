namespace Travel.Tests
{
	using NUnit.Framework;
	using Core.Controllers;
	using Entities;
	using Entities.Airplanes;
	using Entities.Contracts;
	using Entities.Items;

    [TestFixture]
    public class FlightControllerTests
    {
        [Test]
        public void SuccessfulTrip()
        {
            IPassenger[] passengers = new IPassenger[10];

            for (int i = 0; i < passengers.Length;)
            {
                passengers[i] = new Passenger($"Passenger{++i}");
            }

            var airplane = new LightAirplane();

            foreach (var passenger in passengers)
            {
                airplane.AddPassenger(passenger);
            }

            var trip = new Trip("Sofia", "London", airplane);

            var airport = new Airport();

            airport.AddTrip(trip);

            var flightController = new FlightController(airport);

            var bag = new Bag(passengers[1], new[] { new Colombian() });

            passengers[1].Bags.Add(bag);

            var completedTrip = new Trip("Sofia", "Varna", new LightAirplane());
            completedTrip.Complete();

            airport.AddTrip(completedTrip);

            var actualResult = flightController.TakeOff();

            var expectedResult =
                "SofiaLondon1:\r\nOverbooked! Ejected Passenger2, Passenger1, Passenger4, Passenger8, Passenger9\r\nConfiscated 1 bags ($50000)\r\nSuccessfully transported 5 passengers from Sofia to London.\r\nConfiscated bags: 1 (1 items) => $50000";

            Assert.That(actualResult, Is.EqualTo(expectedResult));

            Assert.That(trip.IsCompleted, Is.True);
        }
    }
}
