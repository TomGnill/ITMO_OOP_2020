using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MRace
{
    class Transport
    {
        public struct LandTransport
        {
            public int Speed;
            public double TimeToRelax;
            public double[] RelaxTime;

        }
       public struct AirTransport
        {
            public int Speed;
            public double[] DistanceReducer;

        }
       public static double ReduceDistForIndividual(double distance, AirTransport someAir)
       {
           double realdist = 0;

           if (someAir.DistanceReducer.Length == 1)
           {
               realdist = distance - distance * someAir.DistanceReducer[0];
           }

           if (someAir.DistanceReducer.Length == 2)
           {
               double Reducepers =  distance / 100000 ;
               realdist = distance - distance * Reducepers;

           }

           if (someAir.DistanceReducer.Length == 4)
           {
               if (distance < 1000)
               {
                   realdist = distance;
               }

               if (distance < 5000 && distance >1000)
               {
                   realdist = distance - distance * someAir.DistanceReducer[1];
               }

               if (distance < 10000 & distance >5000)
               {
                   realdist = distance - distance * someAir.DistanceReducer[2];
               }

               if (distance > 10000)
               {
                   realdist = distance - distance * someAir.DistanceReducer[3];
               }
           }

           return realdist;
       }
    }

    class TypeRace
    {
        public List<double> StartLandRace(List<Transport.LandTransport> members, double distance)
        {  
            List<double> RaceResult = new List<double>();
            double result;
            
            int RelaxTimes;
          
            foreach (Transport.LandTransport member in members)
            {
                double PlusRelaxTime=0;
                double ConstRelax=0;
                result = distance / member.Speed;
                RelaxTimes = Convert.ToInt32(result / member.TimeToRelax)+1;
                for (int index = 0; index < RelaxTimes-1; index++)
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
        public  (Transport.LandTransport, double) SpotWinnerLand(List<double> Results, List<Transport.LandTransport> members)
        {
            var nullwinner = new Transport.LandTransport
            {
                Speed = 0,
                TimeToRelax = 0,
                RelaxTime = new Double[1] {0}
            };

            (Transport.LandTransport, double) winner = (nullwinner,  0);
        
            double BestResult = 100000;
            int indexator=0;
            foreach (Transport.LandTransport member in members)
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

        public List<double> StartAirRace(List<Transport.AirTransport> members, int distance)
        {
            List<double> RaceResult = new List<double>();
            double result = 0;
            foreach (Transport.AirTransport member in members)
            {
                result = Transport.ReduceDistForIndividual(distance, member) / member.Speed;
                RaceResult.Add(result);

            }
            return RaceResult;
        }

        public (Transport.AirTransport, double) SpotWinnerAir(List<double> Results, List<Transport.AirTransport> members)
        {
            var nullwinner = new Transport.AirTransport
            {
                Speed = 0,
                DistanceReducer = new double[] {0}
            };

            (Transport.AirTransport, double) winner = (nullwinner, 0);

            double BestResult = 100000;
            int indexator = 0;
            foreach (Transport.AirTransport member in members)
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
