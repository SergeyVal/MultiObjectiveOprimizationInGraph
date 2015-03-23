namespace MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts
{
    public class Id
    {
        private readonly int value;

        private static int _nextId = 0;

        public Id(int value)
        {
            this.value = value;
            if (value > _nextId) _nextId = value + 1;
        }
        
        public static Id GetNextId()
        {
            var retId = new Id(_nextId);
            _nextId++;
            return retId;
        }

        protected bool Equals(Id other)
        {
            return value == other.value;
        }
        
        public static bool operator ==(Id left, Id right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id left, Id right)
        {
            return !left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Id)obj);
        }

        public override int GetHashCode()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
