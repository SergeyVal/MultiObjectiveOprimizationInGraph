using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiObjectiveOptimizationLib.Solver.Clustering
{
    public class ClusterDistances<T>
    {
        private readonly Dictionary<ClusterKey, double> _distances = new Dictionary<ClusterKey,double>();

        public double? Get(Cluster<T> first, Cluster<T> second)
        {
            if (!Contains(first, second)) return null;
            return _distances.First(x => x.Key.Equals(first, second)).Value;
        }

        public IEnumerable<Tuple<Cluster<T>,double>> Get(Cluster<T> cluster)
        {
            return _distances.Where(x => x.Key.Contais(cluster)).Select(x => new Tuple<Cluster<T>, double>(x.Key.Other(cluster),x.Value));
        } 

        public void Add(Cluster<T> first, Cluster<T> second, double distance)
        {
            if(!Contains(first,second))
                _distances.Add(new ClusterKey(first, second), distance);
        }

        public bool Contains(Cluster<T> cluster)
        {
            return  _distances.Any(x => x.Key.Contais(cluster));
        }

        public bool Contains(Cluster<T> first, Cluster<T> second)
        {
            return _distances.Any(x => x.Key.Equals(first,second));
        }

        public void Remove(Cluster<T> cluster)
        {
            var keys = _distances.Keys.Where(x => x.Contais(cluster));
            foreach (var clusterKey in keys)
            {
                _distances.Remove(clusterKey);
            }
        }

        public void Remove(IEnumerable<Cluster<T>> clusters)
        {
            var keys = new List<ClusterKey>();
            foreach (var cluster in clusters)
            {
                keys.AddRange(_distances.Keys.Where(x => x.Contais(cluster)));
            }
            foreach (var clusterKey in keys)
            {
                _distances.Remove(clusterKey);
            }
        }

        public void Clear()
        {
            _distances.Clear();
        }

        private class ClusterKey
        {
            public Cluster<T> First { get; private set; }
            public Cluster<T> Second { get; private set; }

            public ClusterKey(Cluster<T> first, Cluster<T> second)
            {
                First = first;
                Second = second;
            }

            public bool Contais(Cluster<T> cluster)
            {
                return First == cluster || Second == cluster;
            }

            public bool Equals(Cluster<T> first, Cluster<T> second)
            {
                return (First == first && Second == second) || (First == second && Second == first);
            }

            public Cluster<T> Other(Cluster<T> cluster)
            {
                if (First == cluster) return Second;
                if (Second == cluster) return First;
                return null;
            }
        }
    }
}