using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiObjectiveOptimizationLib.Solver.Clustering
{
    public class ClusteringOperator<T>
    {
        private readonly int _countOfClusters;
        private Dictionary<T, Vector<double>> _objectivesValues;

        
        public ClusteringOperator(int countOfClusters)
        {
            _countOfClusters = countOfClusters;
        }


        public List<T> Clustering(List<T> nonDominantPopulation, Dictionary<T,Vector<double>> objectivesValues )
        {
            _objectivesValues = objectivesValues;
            List<Cluster<T>> clusters = nonDominantPopulation.Select(x =>new Cluster<T> {x}).ToList();
            CalculateAllDistances(clusters);
            while (clusters.Count > _countOfClusters)
            {
                var nearest = FindNearestClusters(clusters,objectivesValues);
                clusters.RemoveAll(x => x.Equals(nearest.Item1) || x.Equals(nearest.Item2));
                clusters.Add(nearest.Item1.Merge(nearest.Item2));
            }
            Cluster<T>.ClearDistances();
            return clusters.Select(x => x.Centroid(_objectivesValues)).ToList();
        }

        private Tuple<Cluster<T>,Cluster<T>> FindNearestClusters(List<Cluster<T>> clusters, Dictionary<T,Vector<double>> objectivesValues)
        {

            var minFirst = clusters[0];
            var minSecond = clusters[1];
            var minValue = double.MaxValue;
            for (int i = 0; i < clusters.Count; i++)
            {
                for (int j = i+1; j < clusters.Count; j++)
                {
                    var dist = clusters[i].Distance(clusters[j], objectivesValues);
                    if (!(dist < minValue)) continue;
                    minFirst = clusters[i];
                    minSecond = clusters[j];
                    minValue = dist;
                }
            }
            return new Tuple<Cluster<T>, Cluster<T>>(minFirst, minSecond);
        }
        
        private void CalculateAllDistances(List<Cluster<T>> clusters)
        {
            for (int i = 0; i < clusters.Count; i++)
            {
                for (int j = i+1; j < clusters.Count; j++)
                {
                    clusters[i].Distance(clusters[j],_objectivesValues);
                }
            }
        }

        


    }
}