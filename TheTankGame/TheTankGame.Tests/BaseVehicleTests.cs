namespace TheTankGame.Tests
{
    using Entities.Miscellaneous;
    using Entities.Miscellaneous.Contracts;
    using Entities.Parts;
    using Entities.Parts.Contracts;
    using Entities.Vehicles;
    using Entities.Vehicles.Contracts;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;

    [TestFixture]
    public class BaseVehicleTests
    {
        [Test]
        public void SetNullOrEmptyModelShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>( () => vehicle = new Revenger("", 10, 10, 10, 10, 10, new VehicleAssembler()), "Model cannot be null or white space!");
        }

        [Test]
        public void SetZeroWeightShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("model", 0, 10, 10, 10, 10, new VehicleAssembler()), "Weight cannot be less or equal to zero!");
        }

        [Test]
        public void SetNegativeWeightShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("model", -10, 10, 10, 10, 10, new VehicleAssembler()), "Weight cannot be less or equal to zero!");
        }


        [Test]
        public void SetZeroPriceShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("model", 10, 0, 10, 10, 10, new VehicleAssembler()), "Weight cannot be less or equal to zero!");
        }

        [Test]
        public void SetNegativePriceShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("model", 10, -10, 10, 10, 10, new VehicleAssembler()), "Price cannot be less or equal to zero!");
        }

        [Test]
        public void SetNegativeAttackShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("model", 10, 10, -10, 10, 10, new VehicleAssembler()), "Attack cannot be less than zero!");
        }

        [Test]
        public void SetNegativeDefenceShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("model", 10, 10, 10, -10, 10, new VehicleAssembler()), "Defense cannot be less than zero!");
        }

        [Test]
        public void SetNegativeHitPointsShouldThrowError()
        {
            IVehicle vehicle = null;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("model", 10, 10, 10, 10, -10, new VehicleAssembler()), "HitPoints cannot be less than zero!");
        }

        [Test]
        public void TotalWeightShouldReturnCorrectValue()
        {
            var arsenalPart = new ArsenalPart("model", 100, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            assembler.AddArsenalPart(arsenalPart);
            var assemblerTotalWeight = assembler.TotalWeight;

            IVehicle v = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            var expectedVehicleTotalWeight = v.Weight + assemblerTotalWeight;

            var actualVehicleTotalWeigth = v.TotalWeight;

            Assert.AreEqual(expectedVehicleTotalWeight, actualVehicleTotalWeigth);
        }

        [Test]
        public void TotalPriceShouldReturnCorrectValue()
        {
            var arsenalPart = new ArsenalPart("model", 10, 110, 10);
            IAssembler assembler = new VehicleAssembler();
            assembler.AddArsenalPart(arsenalPart);
            var assemblerTotalPrice = assembler.TotalPrice;

            IVehicle v = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            var expectedVehicleTotalPrice = v.Price + assemblerTotalPrice;

            var actualVehicleTotalPrice = v.TotalPrice;

            Assert.AreEqual(expectedVehicleTotalPrice, actualVehicleTotalPrice);
        }

        [Test]
        public void TotalAttackShouldReturnCorrectValue()
        {
            var arsenalPart = new ArsenalPart("model", 10, 10, 100);
            IAssembler assembler = new VehicleAssembler();
            assembler.AddArsenalPart(arsenalPart);

            IVehicle v = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            var expectedVehicleTotal = v.Attack + assembler.TotalAttackModification;

            var actualVehicleTotal = v.TotalAttack;

            Assert.AreEqual(expectedVehicleTotal, actualVehicleTotal);
        }

        [Test]
        public void TotalDefenseShouldReturnCorrectValue()
        {
            var part = new ShellPart("model", 10, 10, 100);
            IAssembler assembler = new VehicleAssembler();
            assembler.AddShellPart(part);

            IVehicle v = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            var expectedVehicleTotal = v.Defense + assembler.TotalDefenseModification;

            var actualVehicleTotal = v.TotalDefense;

            Assert.AreEqual(expectedVehicleTotal, actualVehicleTotal);
        }

        [Test]
        public void TotalHitPointsShouldReturnCorrectValue()
        {
            var part = new EndurancePart("model", 10, 10, 100);
            IAssembler assembler = new VehicleAssembler();
            assembler.AddEndurancePart(part);

            IVehicle v = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            var expectedVehicleTotal = v.HitPoints + assembler.TotalHitPointModification;

            var actualVehicleTotal = v.TotalHitPoints;

            Assert.AreEqual(expectedVehicleTotal, actualVehicleTotal);
        }

        [Test]
        public void PartsPropertyShouldReturnCollectionWithAllTypesOfParts()
        {
            var part1 = new EndurancePart("model", 10, 10, 10);
            var part2 = new ShellPart("model", 10, 10, 10);
            var part3 = new ArsenalPart("model", 10, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            assembler.AddEndurancePart(part1);
            assembler.AddShellPart(part2);
            assembler.AddArsenalPart(part3);

            IVehicle v = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            var expectedCount = 3;
            var actualCount = v.Parts.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddArsenalPartShouldAddThePartToAssembler()
        {
            //Arrange
            var part = new ArsenalPart("part", 10, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            IVehicle vehicle = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            //Action
            vehicle.AddArsenalPart(part);
            bool assemblerHasPart = assembler.ArsenalParts.Any(ap => ap.Equals(part));

            //Assert
            Assert.AreEqual(true, assemblerHasPart);
        }

        [Test]
        public void AddArsenalPartShouldAddThePartToOrderedParts()
        {
            //Arrange
            IPart part = new ArsenalPart("part", 10, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            IVehicle vehicle = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            //Action
            vehicle.AddArsenalPart(part);

            FieldInfo orderedPartsField = vehicle.GetType().BaseType.GetField("orderedParts", BindingFlags.NonPublic | BindingFlags.Instance);

            List<string> orderedParts = (List<string>)orderedPartsField.GetValue(vehicle);

            //Assert
            Assert.AreEqual(true, orderedParts[0] == part.Model);
        }

        [Test]
        public void AddShellPartShouldAddThePartToAssembler()
        {
            //Arrange
            var part = new ShellPart("part", 10, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            IVehicle vehicle = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            //Action
            vehicle.AddShellPart(part);
            bool assemblerHasPart = assembler.ShellParts.Any(ap => ap.Equals(part));

            //Assert
            Assert.AreEqual(true, assemblerHasPart);
        }

        [Test]
        public void AddShellPartShouldAddThePartToOrderedParts()
        {
            //Arrange
            IPart part = new ShellPart("part", 10, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            IVehicle vehicle = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            //Action
            vehicle.AddShellPart(part);

            FieldInfo orderedPartsField = vehicle.GetType().BaseType.GetField("orderedParts", BindingFlags.NonPublic | BindingFlags.Instance);

            List<string> orderedParts = (List<string>)orderedPartsField.GetValue(vehicle);

            //Assert
            Assert.AreEqual(true, orderedParts[0] == part.Model);
        }

        [Test]
        public void AddEndurancePartShouldAddThePartToAssembler()
        {
            //Arrange
            var part = new EndurancePart("part", 10, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            IVehicle vehicle = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            //Action
            vehicle.AddEndurancePart(part);
            bool assemblerHasPart = assembler.EnduranceParts.Any(ap => ap.Equals(part));

            //Assert
            Assert.AreEqual(true, assemblerHasPart);
        }

        [Test]
        public void AddEndurancePartShouldAddThePartToOrderedParts()
        {
            //Arrange
            IPart part = new EndurancePart("part", 10, 10, 10);
            IAssembler assembler = new VehicleAssembler();
            IVehicle vehicle = new Revenger("revenger", 10, 10, 10, 10, 10, assembler);

            //Action
            vehicle.AddEndurancePart(part);

            FieldInfo orderedPartsField = vehicle.GetType().BaseType.GetField("orderedParts", BindingFlags.NonPublic | BindingFlags.Instance);

            List<string> orderedParts = (List<string>)orderedPartsField.GetValue(vehicle);

            //Assert
            Assert.AreEqual(true, orderedParts[0] == part.Model);
        }

        [Test]
        public void VechileToStringMethodShouldrReturnCorrectString()
        {
            IVehicle vehicle = new Revenger("Gosho", 112, 12, 12, 12, 12, new VehicleAssembler());

            vehicle.AddArsenalPart(new ArsenalPart("Top", 12, 12, 2));
            vehicle.AddArsenalPart(new ArsenalPart("Test", 22, 1, 3));
            vehicle.AddEndurancePart(new EndurancePart("Hit", 2, 52, 1));
            vehicle.AddShellPart(new ShellPart("Shelche", 2, 52, 1));

            var expectedResult = "Revenger - Gosho\r\nTotal Weight: 150,000\r\nTotal Price: 129,000\r\nAttack: 17\r\nDefense: 13\r\nHitPoints: 13\r\nParts: Top, Test, Hit, Shelche";
            var actualResult = vehicle.ToString();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}