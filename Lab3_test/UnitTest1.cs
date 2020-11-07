using System.Collections.Generic;
using NUnit.Framework;
using MRace;

namespace Lab3_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test()]
        public void Test1()
        {
            List<LandTransport> members = new List<LandTransport>();
            Bactrian camel1 = new Bactrian();
            Centavr cent = new Centavr();
            SuperBoots runner = new SuperBoots();

            SpeedCamel camel2 = new SpeedCamel();
            members.Add(camel1);
            members.Add(camel2);
            members.Add(cent);
            members.Add(runner);


            TypeRace newRace = new TypeRace();
            var startRace = newRace.StartLandRace(members, 4000);
            var winner = newRace.SpotWinnerLand(startRace, members);

            Assert.AreEqual(175.5, winner.Item2);
        }
        [Test()]
        public void Test2()
        {

            List<AirTransport> Airmembers = new List<AirTransport>();
            HarryBroom harry = new HarryBroom();
            Yagalimousine Yaga = new Yagalimousine();
            MagicCarpet Alladin = new MagicCarpet();
            Airmembers.Add(harry);
            Airmembers.Add(Yaga);
            Airmembers.Add(Alladin);


            TypeRace newAirRace = new TypeRace();
            var startAirRace = newAirRace.StartAirRace(Airmembers, 8000);
            var Airwinner = newAirRace.SpotWinnerAir(startAirRace, Airmembers);

            Assert.AreEqual(368, Airwinner.Item2);
        }
    }
}