using System;
using System.Collections.Generic;

namespace MRace
{
    class Program
    {
        
        static void Main(string[] args)
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
            Console.WriteLine($"{winner.Item2}");



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
            Console.WriteLine($"{Airwinner.Item2}");

        }
    }
}
