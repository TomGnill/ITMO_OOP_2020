using System;

namespace MRace
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var Bactrian = new Transport.LandTrasport
            {
                Speed = 10,
                TimeToRelax = 30,
                SecondRelaxTime = 8,
                FerstRelaxTime = 5
            };
            var SpeedCalel = new Transport.LandTrasport
            {
                Speed = 40,
                TimeToRelax = 10,
                SecondRelaxTime = 6.5,
                FerstRelaxTime = 5
            };
            var Centavr = new Transport.LandTrasport
            {
                Speed = 15,
                TimeToRelax = 8,
                SecondRelaxTime = 2,
                FerstRelaxTime = 2

            };
            var SuperBoots = new Transport.LandTrasport
            {
                Speed = 6,
                TimeToRelax = 60,
                SecondRelaxTime = 10,
                FerstRelaxTime = 5
            };
            Console.WriteLine("Hello World!");
        }
    }
}
