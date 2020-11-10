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
            Bactrian camel1 = new Bactrian();
            Centavr cent = new Centavr();
            SuperBoots runner = new SuperBoots();
            SpeedCamel camel2 = new SpeedCamel();
            List<Transport> Allmembers1 = new List<Transport>();
            Allmembers1.Add(camel1);
            Allmembers1.Add(cent);
            Allmembers1.Add(runner);
            Allmembers1.Add(camel2);

            TypeRace newRace1 = new TypeRace();
            var StartNewRace1 = newRace1.StartRace(Allmembers1, 4000);
            var Allwinner1 = newRace1.SpotLand(StartNewRace1, Allmembers1);
            Assert.AreEqual(175.5, Allwinner1.Item2);
        }
        [Test()]
        public void Test2()
        {

            HarryBroom harry = new HarryBroom();
            Yagalimousine Yaga = new Yagalimousine();
            MagicCarpet Alladin = new MagicCarpet();
            List<Transport> Allmembers = new List<Transport>();
            Allmembers.Add(harry);
            Allmembers.Add(Yaga);
            Allmembers.Add(Alladin);


            TypeRace newRace = new TypeRace();
            var StartNewRace = newRace.StartRace(Allmembers, 8000);
            var Allwinner = newRace.SpotLand(StartNewRace, Allmembers);

            Assert.AreEqual(368, Allwinner.Item2);
        }
    }
}