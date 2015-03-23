using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using MultiObjectiveOptimizationDrawer.Events;
using MultiObjectiveOptimizationDrawer.Rendering;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationDrawer.Data
{
    public class NodeCollectionStorage<T> : ObservableCollection<Point> 
        where T: INodeCollection, new()
    {
        private T _nodeCollection = default(T);
        private Point _start;
        private Point _end;
        public Point Start {
            get { return _start; }
            set
            {
                if (_start != default(Point))
                {
                    OnPointPropertyChanged(_start);
                    _start = value;
                }
                else _start = value;
            }
        }
        public Point End {
            get { return _end; }
            set
            {
                if (_end != default(Point))
                {
                    OnPointPropertyChanged(_end);
                    _end = value;
                }
                else _end = value;
            }
        }

        public event PropertyChanged PointPropertyChanged;

        public NodeCollectionStorage():base()
        {
            CollectionChanged += CollectionChangedHandler;
        }

        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            _nodeCollection = default(T);
        }

        public Node GetStartNode()
        {
            if (Start == default(Point)) throw new Exception("Property not setted");
            return GetNode(Start);
        }

        public Node GetEndNode()
        {
            if(End==default(Point)) throw new Exception("Property not setted");
            return GetNode(End);
        }
        
        private Node GetNode(Point point)
        {
            CreateNodeCollection();
            return _nodeCollection.GetNode(point.X, point.Y);
        }

        public void SetNodeCollection(T collection)
        {
            Clear();
            foreach (var node in collection)
            {
                Add(CreatePoint(node));
            }
        }

        public T GetNodeCollection()
        {
            CreateNodeCollection();
            return _nodeCollection;
        }

        private void CreateNodeCollection()
        {
            if (_nodeCollection != null) return;
            _nodeCollection = new T();
            this.ForEach(x => _nodeCollection.Add(CreateNode(x)));
        }

        public new void Clear()
        {
            var count = Count;
            for (int i = 0; i < count; i++)
            {
                Remove(this[0]);
            }
            Start = default (Point);
            End = default(Point);
        }

        private Node CreateNode(Point point)
        {
            return new Node(new Coordinates(point.X, point.Y));
        }

        private Point CreatePoint(Node node)
        {
            return new Point(node.Coordinates.X,node.Coordinates.Y);
        }

        protected virtual void OnPointPropertyChanged(Point point)
        {
            var handler = PointPropertyChanged;
            if (handler != null) handler(this, new PointChangedArgs(point));
        }

        
    }
}