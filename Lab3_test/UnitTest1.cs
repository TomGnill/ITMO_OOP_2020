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
            var Bactrian = new Transport.LandTransport
            {
                Speed = 10,
                TimeToRelax = 30,
                RelaxTime = new double[2] { 5, 8 }
            };
            var SpeedCalel = new Transport.LandTransport
            {
                Speed = 40,
                TimeToRelax = 10,
                RelaxTime = new double[3] { 5, 6.5, 8 }

            };
            var Centavr = new Transport.LandTransport
            {
                Speed = 15,
                TimeToRelax = 8,
                RelaxTime = new double[1] { 2 }

            };
            var SuperBoots = new Transport.LandTransport
            {
                Speed = 6,
                TimeToRelax = 60,
                RelaxTime = new double[2] { 10, 5 }
            };
            List<Transport.LandTransport> members = new List<Transport.LandTransport>();
            members.Add(SpeedCalel);
            members.Add(SuperBoots);
            members.Add(Centavr);
            members.Add(Bactrian);

            TypeRace newRace = new TypeRace();
            var startRace = newRace.StartLandRace(members, 4000);
            var winner = newRace.SpotWinnerLand(startRace, members);

            Assert.AreEqual(175.5, winner.Item2);
        }
        [Test()]
        public void Test2()
        {
            var MagicCarpet = new Transport.AirTransport
            {
                Speed = 10,
                DistanceReducer = new double[4] { 0, 0.03, 0.10, 0.05 }
            };
            var Yagalimousine = new Transport.AirTransport
            {
                Speed = 8,
                DistanceReducer = new double[1] { 0.06 }
            };
            var HarryBroom = new Transport.AirTransport
            {
                Speed = 20,
                DistanceReducer = new double[2] { 0.01, 0.01 }
            };

            List<Transport.AirTransport> Airmembers = new List<Transport.AirTransport>();
            Airmembers.Add(MagicCarpet);
            Airmembers.Add(Yagalimousine);
            Airmembers.Add(HarryBroom);

            TypeRace newAirRace = new TypeRace();
            var startAirRace = newAirRace.StartAirRace(Airmembers, 8000);
            var Airwinner = newAirRace.SpotWinnerAir(startAirRace, Airmembers);

            Assert.AreEqual(368, Airwinner.Item2);
        }
    }
}