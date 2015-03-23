using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators
{
    public class EllipseCreator
    {
        private Brush _outlineBrush;
        private Brush _fillBrush;
        private readonly double _radius;
        private readonly double _opacity;
        public ContextMenu ContextMenu { get; set; }

        public EllipseCreator(Brush outlineBrush, Brush fillBrush, double radius, double opacity)
        {
            _outlineBrush = outlineBrush;
            _fillBrush = fillBrush;
            _radius = radius;
            _opacity = opacity;
        }

        public Ellipse Create(Point point)
        {
            var el = new Ellipse
            {
                Width = 2*_radius,
                Height = 2*_radius,
                Opacity = _opacity,
                Stroke = _outlineBrush,
                Fill = _fillBrush,
                Margin = new Thickness(point.X - _radius, point.Y - _radius, 0, 0),
            };
            if (ContextMenu != null) el.ContextMenu = ContextMenu;
            return el;
        }

        public void SetNextRandomOutlineBrush()
        {
            _outlineBrush= BrushesList.NextRandom();
        }
        public void SetNextRandomFillBrush()
        {
            _fillBrush = BrushesList.NextRandom();
        }
        
        
    }
}