using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MRace
{
    class Transport
    {

       public struct LandTrasport
        {
            public double Speed;
            public double TimeToRelax;
            public  double FerstRelaxTime;
            public double SecondRelaxTime;

        }

       public double RestDuration(double firstRelax, double secondRelax, double nextRelax)
       {

       }
       public struct AirTransport
        {
            public double Speed;
            public double Distance;

        }

        public double RealDistance(AirTransport transport, double RaceDistance)
        {
            double realdist = transport.Distance * RaceDistance;

            return realdist;

        }
        //умножение процента на итоговую дистанцию
    }
}
