using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.NodeCollections.GraphParts
{
    public class Node
    {
        public Id Id { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public Node Parent { get; set; }
        
        public Node(Coordinates coordinates)
        {
            Id = Id.GetNextId();
            Coordinates = coordinates;
            Parent = null;
        }

        public Node(double x, double y)
        {
            Id = Id.GetNextId();
            Coordinates = new Coordinates(x,y);
            Parent = null;
        }

        public Node(Id id, Coordinates coordinates)
        {
            Id = id;
            Coordinates = coordinates;
            Parent = null;
        }

        public override string ToString()
        {
            return Id + " - (" + Coordinates + ")";
        }
    }
}

