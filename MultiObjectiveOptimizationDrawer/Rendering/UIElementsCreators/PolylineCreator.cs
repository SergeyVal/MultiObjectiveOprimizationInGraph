using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators
{
    public class PolylineCreator
    {
        public Brush Brush { get; set; }
        private readonly double _brushThickness;
        private readonly double _opacity;

        public PolylineCreator(Brush brush, double brushThickness, double opacity)
        {
            Brush = brush;
            _brushThickness = brushThickness;
            _opacity = opacity;
        }

        public Polyline Create(Route route)
        {
            Polyline polyline = new Polyline
            {
                Stroke = Brush,
                StrokeThickness = _brushThickness,
                Opacity = _opacity,
                Points = CreatePointCollection(route)
            };

            return polyline;
        }

        public Polyline Create(IEnumerable<Point> points)
        {
            Polyline polyline = new Polyline
            {
                Stroke = Brush,
                StrokeThickness = _brushThickness,
                Opacity = _opacity,
                Points = new PointCollection(points)
            };
            return polyline;
        }

        private PointCollection CreatePointCollection(IEnumerable<Node> nodes)
        {
            return new PointCollection(nodes.Select(x => new Point(x.Coordinates.X, x.Coordinates.Y)));
        }

        public void SetNextRandomBrush()
        {
            Brush = BrushesList.NextRandom();
        }

        
    }
}