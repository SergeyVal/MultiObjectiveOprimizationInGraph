using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators
{
    public class LineCreator
    {
        private readonly Brush _linkBrush;
        private readonly double _brushThickness;
        private readonly double _opacity;

        public LineCreator(Brush linkBrush, double brushThickness, double opacity)
        {
            _linkBrush = linkBrush;
            _brushThickness = brushThickness;
            _opacity = opacity;
        }

        public Line Create(Point first, Point second)
        {
            return new Line
            {
                X1 = first.X,
                Y1 = first.Y,
                X2 = second.X,
                Y2 = second.Y,
                Stroke = _linkBrush,
                StrokeThickness = _brushThickness,
                Opacity = _opacity
            };

        } 
    }
}