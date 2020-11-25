using System;
using System.Collections.Generic;
using System.Text;

namespace PudelkoLibrary
{
    class Pudelko : IFormattable
    {
        private double _A;
        private double _B;
        private double _C;

        public double A { get => _A / 1000; }
        public double B { get => _B / 1000; }
        public double C { get => _C / 1000; }



        private UnitOfMeasure unit;
        public Pudelko()
        {
            A = 100;
            B = 100;
            C = 100;
            this.unit = UnitOfMeasure.meter;
        }

        private double ParseToMilimeter(double value, UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    return value;
                case UnitOfMeasure.centimeter:
                    return value * 10;
                case UnitOfMeasure.meter:
                    return value + 1000;
                default: 
                    throw new ArgumentOutOfRangeException();
            }
        }
        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if (a <= 0 || b <= 0 || c <= 0) 
                throw new ArgumentOutOfRangeException();
            if(ParseToMilimeter(a, unit) > 10000 || ParseToMilimeter(b, unit) > 10000 || ParseToMilimeter(c, unit) > 10000)
                throw new ArgumentOutOfRangeException();
            _A = ParseToMilimeter(a, unit);
            _B = ParseToMilimeter(b, unit);
            _C = ParseToMilimeter(c, unit);

        }

        public override string ToString()
        {
            return ToString("m");
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format) {
                case "mm":
                    return $"{_A} m × {_B} m × {_C} m ×";
                case "cm":
                    return $"{_A / 10} m × {_B / 10} m × {_C /10} m ×";
                case "m":
                    return $"{_A / 1000} m × {_B / 1000} m × {_C / 1000} m ×";
                default:
                    throw new FormatException();
            }

        }
    }
}
