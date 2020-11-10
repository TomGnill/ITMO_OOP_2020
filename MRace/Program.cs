using System;
using System.Collections.Generic;

namespace MRace
{
    class Program
    {
        
        static void Main(string[] args)
        {
           
          
            Bactrian camel1 = new Bactrian();
            Centavr cent = new Centavr();
            SuperBoots runner = new SuperBoots();
            SpeedCamel camel2 = new SpeedCamel();
          
           
           
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
            Console.WriteLine($"{Allwinner.Item2}");

            List<Transport> Allmembers1 = new List<Transport>();
            Allmembers1.Add(camel1);
            Allmembers1.Add(cent);
            Allmembers1.Add(runner);
            Allmembers1.Add(camel2);
            Allmembers1.Add(harry);

            TypeRace newRace1 = new TypeRace();
            var StartNewRace1 = newRace1.StartRace(Allmembers1, 4000);
            var Allwinner1 = newRace1.SpotLand(StartNewRace1, Allmembers1);
            Console.WriteLine($"{Allwinner1.Item2}");


        }
    }
}
