// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to your project (entities/controllers/etc)

using System;
using FestivalManager.Core.Controllers;
using FestivalManager.Core.Controllers.Contracts;
using FestivalManager.Entities;
using FestivalManager.Entities.Contracts;
using FestivalManager.Entities.Instruments;
using FestivalManager.Entities.Sets;

namespace FestivalManager.Tests
{
	using NUnit.Framework;

	[TestFixture]
	public class SetControllerTests
    {
		[Test]
	    public void SetControllerShouldReturnFailMessage()
		{
            IStage stage = new Stage();
            ISetController setController = new SetController(stage);

            ISet set = new Short("Set1");
            stage.AddSet(set);
		   
		    string actualResult = setController.PerformSets();
		    string expectedResult = "1. Set1:\r\n-- Did not perform";

            Assert.AreEqual( expectedResult ,actualResult);

		}

        [Test]
        public void SetControllerShouldReturnSuccessMessage()
        {
            var stage = new Stage();

            var performer = new Performer("Ivo", 26);

            IInstrument instrument = new Guitar();

            performer.AddInstrument(instrument);

            stage.AddPerformer(performer); //

            ISong song = new Song("Chico loco", new TimeSpan(0, 2, 30));

            stage.AddSong(song); //

            ISet set = new Short("ShortSet");
            stage.AddSet(set);

            var setController = new SetController(stage);

            var actualResult = setController.PerformSets();
            var expectedResult = "1. ShortSet:\r\n-- Did not perform";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void SetControllerShouldWearDownInstruments()
        {            
            Performer performer = new Performer("Ivo", 26);
            IInstrument instrument = new Guitar();
            performer.AddInstrument(instrument);

            ISet set = new Short("ShortSet");
            set.AddPerformer(performer);
            ISong song = new Song("Chico loco", new TimeSpan(0, 2, 30));
            set.AddSong(song);

            var stage = new Stage();
            stage.AddPerformer(performer);
            stage.AddSong(song);
            stage.AddSet(set);

            var setController = new SetController(stage);

            var instrumentWearBefore = instrument.Wear;

            setController.PerformSets();

            var instrumentWearAfter = instrument.Wear;

            Assert.AreNotEqual(instrumentWearAfter, instrumentWearBefore);
        }
    }
}