﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PudelkoLibrary
{
    class Pudelko : IFormattable
    {
        private double _A;
        private double _B;
        private double _C;
        private UnitOfMeasure unit;


        public double A { get => _A / 1000; }
        public double B { get => _B / 1000; }
        public double C { get => _C / 1000; }

        public Pudelko()
        {
            _A = 100;
            _B = 100;
            _C = 100;
            this.unit = UnitOfMeasure.meter;
        }

        public double Objetosc
        {
            get => Math.Round((_A / 1000) * (_B / 1000) * (_C / 1000));
        }

        public double Pole
        {
            get => Math.Round(2 * ((_A / 1000) * (_B / 1000)) + 2 * ((_A / 1000) * (_C / 1000)) + 2 * ((_C / 1000) * (_B / 1000)), 6);
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

        public bool Equals(Pudelko obj)
        {
            if (_A == obj._A)
            {
                if (_B == obj._B)
                {
                    if (_C == obj._C)
                    {
                        return true;
                    }
                }
                if (_B == obj._C)
                {
                    if (_C == obj._B)
                    {
                        return true;
                    }
                }
            }
            if (_B == obj._A)
            {
                if (_A == obj._B)
                {
                    if (_C == obj._C)
                    {
                        return true;
                    }
                }
                if (_A == obj._C)
                {
                    if (_C == obj._B)
                    {
                        return true;
                    }
                }
            }
            if (_C == obj._A)
            {
                if (_A == obj._B)
                {
                    if (_C == obj._C)
                    {
                        return true;
                    }
                }
                if (_B == obj._C)
                {
                    if (_C == obj._B)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool operator ==(Pudelko obj, Pudelko obj2)
        {
            return obj.Equals(obj2);
        }

        public static bool operator !=(Pudelko obj, Pudelko obj2)
        {
            return obj.Equals(obj2);
        }
        public static Pudelko operator +(Pudelko obj, Pudelko obj2)
        {
            double a = obj._A + obj2._A;
            double b = obj._B + obj2._B;
            double c = obj._C > obj2._C ? obj._C : obj2._C;
            return new Pudelko(a, b, c, UnitOfMeasure.milimeter);

        }

        public static explicit operator double[](Pudelko obj)
        {
            var r = new double[] { obj.A, obj.B, obj.C };
            return r;
        }
        public static implicit operator Pudelko(ValueTuple<int, int, int> wymiary)
        {
            return new Pudelko((double)wymiary.Item1, (double)wymiary.Item2, (double)wymiary.Item3, UnitOfMeasure.milimeter);
        }

        public double this[int index] {
            get
            {
                switch (index) {
                    case 0:
                        return _A;
                    case 1:
                        return _B;
                    case 2:
                        return _C;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        int position = 0;

        public bool MoveNext()
        {
            position++;
            return (position < 3);
        }

        public void Reset()
        {
            position = 0;
        }

        public Object Current
        {
            get
            {
                switch (position)
                {
                    case 0:
                        return _A;
                    case 1:
                        return _B;
                    case 2:
                        return _C;
                    default:
                        return 0;

                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

    }


    public interface IEquatable<Pudelko> { }
    public interface IEnumerable<Pudelko> : System.Collections.IEnumerable { };
}
