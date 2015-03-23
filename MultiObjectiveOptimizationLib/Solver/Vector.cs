using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiObjectiveOptimizationLib.Solver
{
    public class Vector<T>  : List<T> 
        where T : IComparable<T>
    {
        public Vector(List<T> values) : base(values)
        {
        }

        public Vector(IEnumerable<T> values) : base(values)
        {
        }

        public Vector(T value) : base()
        {
            this.Add(value);
        }
        
       

        public static List<Vector<double>> Range(double start, double end, double step)
        {
            if(start>=end) throw new ArgumentException();
            var range = new List<Vector<double>>();
            for (double i = start; i < end; i += step)
            {
                range.Add(new Vector<double>(i));
            }
            return range;
        }
       

        public static Vector<T> operator *(Vector<T> first, Vector<T> second)
        {
            var res = new List<T>(first);
            res.AddRange(second);
            return new Vector<T>(res);
        }

        public static bool operator >(Vector<T> first, Vector<T> second)
        {
            var compare = Compare(first, second);
            return compare.All(i => i == 0 || i == 1) && compare.Any(i => i == 1);
        }

        public static bool operator <(Vector<T> first, Vector<T> second)
        {
            var compare = Compare(first, second);
            return compare.All(i => i == 0 || i == -1) && compare.Any(i => i == -1);
        }
        public static bool operator <=(Vector<T> first, Vector<T> second)
        {
            return Compare(first, second).All(i => i == 0 || i == -1);
        }

        public static bool operator >=(Vector<T> first, Vector<T> second)
        {
            return Compare(first, second).All(i => i == 0 || i == 1);
        }

        public static bool operator ==(Vector<T> first, Vector<T> second)
        {
            return Compare(first, second).All(i => i == 0);
        }

        public static bool operator !=(Vector<T> first, Vector<T> second)
        {
            return !(first == second);
        }

        private static List<int> Compare(Vector<T> first, Vector<T> second)
        {
            if (first.Count != second.Count) throw new ArgumentException("vector sizes are not equal");
            return first.Select((element, index) => element.CompareTo(second[index])).ToList();
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append("(");
            for (int i = 0; i < Count - 1; i++)
            {
                str.Append(this[i] + " ,");
            }
            str.Append(this[Count-1] + ")");
            return str.ToString();
        }
    }
}