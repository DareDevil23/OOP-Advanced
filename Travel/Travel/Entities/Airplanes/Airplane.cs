namespace Travel.Entities.Airplanes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Travel.Entities.Contracts;

    public abstract class Airplane : IAirplane
    {
        private readonly List<IBag> baggageCompartment;
        private readonly List<IPassenger> passangers;

        protected Airplane(int seats, int baggageCompartments)
        {
            this.Seats = seats;
            this.BaggageCompartments = baggageCompartments;

            this.baggageCompartment = new List<IBag>();
            this.passangers = new List<IPassenger>();
        }

        public int BaggageCompartments { get; }

        public IReadOnlyCollection<IBag> BaggageCompartment => this.baggageCompartment.AsReadOnly();

        public bool IsOverbooked => this.passangers.Count > this.Seats;

        public IReadOnlyCollection<IPassenger> Passengers => this.passangers.AsReadOnly();

        public int Seats { get; }

        public void AddPassenger(IPassenger passenger)
        {
            this.passangers.Add(passenger);
        }

        public IPassenger RemovePassenger(int seat)
        {
            var passenger = this.passangers[seat];

            this.passangers.RemoveAt(seat);

            return passenger;
        }

        public IEnumerable<IBag> EjectPassengerBags(IPassenger passenger)
        {
            IList<IBag> allBaggage =
                this.baggageCompartment.Where(b => b.Owner.Username == passenger.Username).ToList();

            foreach (IBag bag in allBaggage)
            {
                this.baggageCompartment.Remove(bag);
            }

            return allBaggage;
        }

        public void LoadBag(IBag bag)
        {
            if (this.baggageCompartment.Count > this.BaggageCompartments)
            {
                throw new InvalidOperationException($"No more bag room in {this.GetType().Name}");
            }

            this.baggageCompartment.Add(bag);
        }
    }
}
