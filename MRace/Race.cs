using System;
using System.Collections.Generic;
using System.Text;

namespace MRace
{
    public class TypeRace
    {
        public List<double> StartLandRace(List<LandTransport> members, double distance)
        {
            List<double> RaceResult = new List<double>();

            double result;

            int RelaxTimes;

            foreach (LandTransport member in members)
            {
                double PlusRelaxTime = 0;
                double ConstRelax = 0;
                result = distance / member.Speed;
                RelaxTimes = Convert.ToInt32(result / member.TimeToRelax) + 1;
                for (int index = 0; index < RelaxTimes - 1; index++)
                {
                    if (member.RelaxTime.Length > index)
                    {
                        PlusRelaxTime += member.RelaxTime[index];
                        ConstRelax = member.RelaxTime[index];
                    }
                    else
                        PlusRelaxTime += ConstRelax;

                }

                result += PlusRelaxTime;
                RaceResult.Add(result);
            }

            return RaceResult;

        }

        public (LandTransport, double) SpotWinnerLand(List<double> Results, List<LandTransport> members)
        {
            LandMystaryLooser looser = new LandMystaryLooser();
            (LandTransport, double) winner = (looser, 0);

            double BestResult = 100000;
            int indexator = 0;
            foreach (LandTransport member in members)
            {
                if (Results[indexator] < BestResult)
                {
                    BestResult = Results[indexator];
                    winner = ((member, BestResult));
                }

                indexator++;
            }

            return winner;
        }

        public List<double> StartAirRace(List<AirTransport> members, double distance)
        {
            List<double> RaceResult = new List<double>();
            double result = 0;
            foreach (AirTransport member in members)
            {
                distance = member.ReduceDistance(distance);
                result = distance / member.Speed;
                RaceResult.Add(result);

            }

            return RaceResult;
        }

        public (AirTransport, double) SpotWinnerAir(List<double> Results, List<AirTransport> members)
        {
            AirLandMystaryLooser looser = new AirLandMystaryLooser();
             (AirTransport, double) winner = (looser, 0);
            double BestResult = 100000;
            int indexator = 0;
            foreach (AirTransport member in members)
            {
                if (Results[indexator] < BestResult)
                {
                    BestResult = Results[indexator];
                    winner = ((member, BestResult));
                }

                indexator++;
            }

            return winner;
        }
    }
}
