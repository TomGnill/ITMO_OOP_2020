
using System;

namespace MRace
{
    public abstract class LandTransport : Transport
    {
        internal readonly int Speed;
        internal double TimeToRelax;
        internal double[] RelaxTime;
        public override double GlSpeed()
        {
            return Speed;
        }

        public LandTransport(int speed, double timeToRelax, double[] relaxTime)
        {
            Speed = speed;
            TimeToRelax = timeToRelax;
            RelaxTime = relaxTime;
        }
        public override double Calc(double distance)
        {
            double result;

            int RelaxTimes;
            double PlusRelaxTime = 0;
            double ConstRelax = 0;
            result = distance / Speed;
            RelaxTimes = Convert.ToInt32(result / TimeToRelax) + 1;
            for (int index = 0; index < RelaxTimes - 1; index++)
            {
                if (RelaxTime.Length > index)
                {
                    PlusRelaxTime += RelaxTime[index];
                    ConstRelax = RelaxTime[index];
                }
                else
                    PlusRelaxTime += ConstRelax;

            }

            result += PlusRelaxTime;
            return result;
        }
    }

    public abstract class AirTransport : Transport
    {
        internal double Speed;
        public abstract double ReduceDistance(double distance);

        public override double GlSpeed()
        {
            return Speed;
        }

        public AirTransport(double speed)
        {
            Speed = speed;
        }

        public override double Calc(double disance)
        {
            double result = 0;

            disance = ReduceDistance(disance);
            result = disance / Speed;
            return result;
        }
    }

    public abstract class Transport
    {
        public abstract double GlSpeed();
        public abstract double Calc(double distance);

    }

  

    public class Bactrian : LandTransport
    {
        public Bactrian() : base(10, 30, new double[2] {5, 8})
        {
        }
    };

    public class SpeedCamel : LandTransport
    {
        public SpeedCamel() : base(40, 10, new double[3] { 5, 6.5, 8 })
        {
        }
    }
    public class Centavr : LandTransport
    {
        public Centavr() : base(15, 8, new double[1] { 2 })
        {
        }
    };

    public class SuperBoots : LandTransport
    {
        public SuperBoots() : base(6, 60, new double[2] { 10, 5 })
        {
        }
    };

    public class MagicCarpet : AirTransport
    {
        public MagicCarpet() : base(10)
        {
        }
        public  override double ReduceDistance(double distance)
        {
            double redDist = 0;
            if (distance < 1000)
            {
                redDist = distance;
            }

            if (distance < 5000 && distance > 1000)
            {
                redDist = distance - distance * 0.03;
            }

            if (distance < 10000 & distance > 5000)
            {
                redDist = distance - distance * 0.10;
            }

            if (distance > 10000)
            {
                redDist = distance - distance * 0.05;
            }

            return redDist;
        }
    }
    public class Yagalimousine : AirTransport
    {
        public Yagalimousine() : base(8)
        {
        }
        public override double ReduceDistance(double distance)
        {
            double redDist = 0;

            redDist = distance - distance * 0.06;


            return redDist;
        }
    }

    public class HarryBroom : AirTransport
    {
        public HarryBroom() : base(20)
        {
        }
        public override double ReduceDistance(double distance)
        {
            double redDist = 0;
            if (distance > 1000)
            {
                double Reducepers = distance / 100000;
                redDist = distance - distance * Reducepers;
            }
            else
            {
                redDist = distance;
            }
            return redDist;
        }
    }

    public class LandMystaryLooser : LandTransport
    {
        public LandMystaryLooser() : base(1, 10000, new double[1] { 100})
        {
        }
    }

    public class AirLandMystaryLooser : AirTransport
    {
        public AirLandMystaryLooser() : base(1000)
        {
        }
        public override double ReduceDistance(double distance)
        {
            double redDist = 0;
            redDist = distance ;
            return redDist;
        }
    }
}
