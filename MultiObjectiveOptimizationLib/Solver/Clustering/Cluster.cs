using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationLib.Solver.Clustering
{
    public class Cluster<T> : List<T>
    {
        private static readonly Func<Vector<double>, Vector<double>, double> _metric = Metrics.EuclideanDistance;

        public static ClusterDistances<T> _distances = new ClusterDistances<T>();
        
        public Cluster(IEnumerable<T> set) : base(set)
        {
            
        }

        public Cluster() : base()
        {
            
        }

        public static void ClearDistances()
        {
            
        }
        
        public Cluster<T> Merge(Cluster<T> otherCluster)
        {
            _distances.Remove(new []{this,otherCluster});
            return new Cluster<T>(otherCluster.Union(this));
        }
        
        public double Distance(Cluster<T> otherCluster, Dictionary<T, Vector<double>> objectivesValues)
        {
            var distTry = _distances.Get(this, otherCluster);
            if (distTry.HasValue) return distTry.Value;
            var distance = CalculateDistance(otherCluster, objectivesValues);
            _distances.Add(this,otherCluster,distance);
            return distance;
        }
        
        private double CalculateDistance(Cluster<T> otherCluster, Dictionary<T, Vector<double>> objectivesValues)
        {
            return SumDistancesBetweenClusters(this, otherCluster, objectivesValues) /
                           (this.Count * otherCluster.Count);
        }
        
        private double SumDistancesBetweenClusters(Cluster<T> first, Cluster<T> second, Dictionary<T, Vector<double>> objectivesValues)
        {
            return first
                .Sum(firstroute => second
                    .Sum(secondroute => _metric(objectivesValues[firstroute], objectivesValues[secondroute])));
        }

        public T Centroid(Dictionary<T, Vector<double>> objectivesValues)
        {
            var min = double.MaxValue;
            var centroid = this[0];
            foreach (var route in this)
            {
                var average = this.Average(x => _metric(objectivesValues[route], objectivesValues[x]));
                if (!(average < min)) continue;
                centroid = route;
                min = average;
            }
            return centroid;
        } 
    }
}