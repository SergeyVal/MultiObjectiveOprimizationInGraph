using System;

namespace MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts
{
    public class Coordinates
    {
        private const double _epsilon = 0.0001;
        
        public double X { get; private set; }

        public double Y { get; private set; }

        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        protected bool Equals(Coordinates other)
        {
            return Math.Abs(X - other.X) < _epsilon && Math.Abs(Y - other.Y) < _epsilon;
        }
        
        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinates)obj);
        }

        public override string ToString()
        {
            return X + ", " + Y;
        }
    }
}