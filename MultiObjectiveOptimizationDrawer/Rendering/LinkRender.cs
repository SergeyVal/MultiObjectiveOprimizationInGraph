using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MultiObjectiveOptimizationDrawer.Data;
using MultiObjectiveOptimizationDrawer.Extensions;
using MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;

namespace MultiObjectiveOptimizationDrawer.Rendering
{
    public class LinkRender
    {
        private readonly NodeCollectionStorage<FullConnectedGraph> _nodeCollectionStorage;
        private readonly Canvas _canvas;
        private readonly LineCreator _lineCreator;

        public LinkRender(NodeCollectionStorage<FullConnectedGraph> nodeCollectionStorage, Canvas canvas, LineCreator lineCreator)
        {
            _nodeCollectionStorage = nodeCollectionStorage;
            _canvas = canvas;
            _nodeCollectionStorage.CollectionChanged += NodeCollectionChangerHandler;
            _lineCreator = lineCreator;
        }

        private void NodeCollectionChangerHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems!=null)
            {
                CreateLines(e.NewItems.OfType<Point>()).ForEach(DrawLine);
            }
            if (e.OldItems!=null)
            {
                e.OldItems.OfType<Point>().ForEach(RemoveLine);
            }
        }

        private void RemoveLine(Point removedPoint)
        {
            var matches = _canvas.Children.OfType<Line>().Where(x => x.Contains(removedPoint));
            _canvas.Children.Remove(matches);
        }

        private void DrawLine(Line line)
        {
            _canvas.Children.Add(line);
            Canvas.SetZIndex(line, int.MinValue);
        } 

        private IEnumerable<Line> CreateLines(IEnumerable<Point> changed)
        {
            List<Line> lines = new List<Line>();
            foreach (var point in _nodeCollectionStorage.Except(changed))
            {
                lines.AddRange(changed.Select(added => CreateLine(point, added)));
            }
            return lines;
        } 

        private Line CreateLine(Point first, Point second)
        {
            return _lineCreator.Create(first, second);
        }
    }
}