using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.FileManager;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationLib.Solver.GeneticAlgorythm
{
    public class GraphInitialSetFactory
    {
        private readonly int _childAddingProbability;

        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        private readonly FullConnectedGraph _graph;

        private readonly int _populationCount;

        public GraphInitialSetFactory(int childAddingProbability, FullConnectedGraph graph, int populationCount)
        {
            _childAddingProbability = childAddingProbability;
            _graph = graph;
            _populationCount = populationCount;
        }

        public List<Route> CreateInitialPopulation(Node start, Node finish)
        {
            try
            {
                return BFS(start, finish);
            }
            catch (Exception e)
            {
                Log.Save(e);
                return new List<Route>() {new Route(new[] {start, finish})};
            }
            
        }

        private List<Route> BFS(Node start, Node finish)
        {
            LinkedList<Node> nodeQueue = new LinkedList<Node>();
            start.Parent = null;
            nodeQueue.AddFirst(start);
            List<Route> outRouts = new List<Route>();
            while (!ExitCondition(nodeQueue,outRouts,start))
            {
                var currentNode = nodeQueue.TakeFirst();  
                if (currentNode==finish)
                {
                    var trace = Trace(currentNode).ToList();
                    outRouts.Add(new Route(trace));
                    continue;
                }
                var childs = GetNotVisitedNodes(start);
                if (childs.Contains(finish))
                {
                    finish.Parent = currentNode;
                    nodeQueue.AddFirst(finish);
                }
                foreach (var child in childs.Where(child => child != start && child != finish))
                {
                    child.Parent = currentNode;
                    nodeQueue.AddLast(child);
                }
                

            }
            ClearGraph();
            return outRouts;
        }

        private void ClearGraph()
        {
            foreach (Node node in _graph)
            {
                node.Parent = null;
            }
        }

        private List<Node> Trace(Node node)
        {
            var route = new List<Node>();
            while (node!=null)
            {
                var temp = node;
                route.Add(node);
                node = node.Parent;
                if (NoChilds(temp)) temp.Parent = null;
            }
            route.Reverse();
            return route;
        }

        private bool NoChilds(Node node)
        {
            return _graph.All(x => x.Parent != node);
        } 

        private List<Node> GetNotVisitedNodes(Node start)
        {
            var got = _graph.Where(x => _random.Next(100) <= _childAddingProbability && x.Parent == null && x !=start).ToList();
            return got.IsEmpty() ? new List<Node> {_graph.First(x => x.Parent==null && x!=start)} : got;
        }
        
        private bool ExitCondition( LinkedList<Node> queue, List<Route> routes, Node start)
        {
            if (queue.Count != 0) return routes.Count == _populationCount;
            queue.AddLast(start);
            ClearGraph();
            return  routes.Count == _populationCount;
        }
    }
}