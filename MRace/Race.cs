using System;
using System.Collections.Generic;
using System.Text;

namespace MRace
{
    public class TypeRace
    {
        public List<double> StartRace(List<Transport> members, double distance)
        {
            List<double> raceResult = new List<double>();
            int CheckSum = members.Count;
            int CheckClassesLand = 0;
            int CheckClassesAir = 0;
            foreach (Transport member in members)
            {
                if (member is LandTransport)
                {
                    CheckClassesLand++;
                }

                if (member is AirTransport)
                {
                    CheckClassesAir++;
                }

                var result = member.Calc(distance);
                raceResult.Add(result);
            }

            if (CheckSum != CheckClassesAir && CheckSum != CheckClassesLand)
            {
                throw new Exception("В гонке учавствуют разные типы транспорта!");
            }
        


        return raceResult;
        }
        public (Transport, double) SpotLand(List<double> Results, List<Transport> members)
        {
            LandMystaryLooser looser = new LandMystaryLooser();

            (Transport, double) winner = (looser, 0);

            double bestResult = 100000;
            int indexator = 0;
            foreach (Transport member in members)
            {
                if (Results[indexator] < bestResult)
                {
                    bestResult = Results[indexator];
                    winner = ((member, bestResult));
                }

                indexator++;
            }

            return winner;
        }
    }
}
